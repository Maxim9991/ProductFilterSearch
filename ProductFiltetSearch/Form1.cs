using BlogForm.Entities;
using BlogForm.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductFiltetSearch
{
    public partial class Form1 : Form
    {
        private readonly EFContext _context;

        public Form1(EFContext context)
        {
            InitializeComponent();
            _context = context;

        }

        private void FilterTestForm_Load(object sender, EventArgs e)
        {

            this.AutoScroll = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;


            Seeder.SeedDatabase(_context);
            var filters = GetFilterNameModels();
            FillCheckedList(filters);
        }

        private IEnumerable<FilterNameModel> GetFilterNameModels()
        {
            List<FilterNameModel> filterNameModels = new List<FilterNameModel>();
            //  Витягуємо елементи FilterName з БД
            var filterNames = from x in this._context.FilterNames.AsQueryable() select x;
            //  Витягуємо елементи FilterNameValue з БД
            var filterNameValue = from x in this._context.FilterNameGroups.AsQueryable() select x;

            var joinedCollection = (from name in filterNames    //  Вибірка усіх елементів FilterName З колекції
                                                                //  Join елементів по Id N-ного елемента і FilterNameId проміжної таблички
                                    join nameValue in filterNameValue on name.Id equals
                                    //  Запис данних у згруповану колекцію oneElementJoinedColl
                                    nameValue.FilterNameId into oneElementJoinedColl
                                    //  Проходження по новій колекції і формування анонімних обєктів
                                    from v in oneElementJoinedColl
                                    select new
                                    {
                                        FilterName = name.Name,
                                        FilterNameId = name.Id,
                                        FilterValue = v.FilterValueOf.Name,
                                        FilterValueId = v.FilterValueId
                                        // Приведення до типу AsEnumerable
                                    }).AsEnumerable();

            //  Вибірка елементів з колекції joinedCollection
            var groupsFilters = from x in joinedCollection
                                    //  Групування данних за ідентифікатором та назвою фільтра і переміщення нової
                                    //  колекції IGrouping до змінної newIGroupingCollection
                                group x by new { x.FilterNameId, x.FilterName } into newIGroupingCollection
                                //  Сортування нової колекції за іменем за спаданням
                                orderby newIGroupingCollection.Key.FilterName descending
                                //  Повернення колекції(груп)
                                select newIGroupingCollection;

            foreach (var item in groupsFilters)
            {
                //  Створення нової моделі де зберігаються дані про фільтр
                //  та його формуються його дочірні елементи
                FilterNameModel model = new FilterNameModel
                {
                    Id = item.Key.FilterNameId,
                    Name = item.Key.FilterName,
                    Children = item.Select(x => new FilterValueModel
                    {
                        Name = x.FilterValue,
                        Id = x.FilterValueId
                    }).ToList()
                };
                //  Додавання до колекції фільтрів нову модель
                filterNameModels.Add(model);
            }



            return filterNameModels;
        }

        private void FillCheckedList(IEnumerable<FilterNameModel> models)
        {
            GroupBox gbFilter;
            CheckedListBox listBox;
            int dy = 13;
            foreach (var item in models)
            {
                gbFilter = new System.Windows.Forms.GroupBox();
                listBox = new System.Windows.Forms.CheckedListBox();
                gbFilter.SuspendLayout();
                // 
                // gbFilter
                // 
                gbFilter.Controls.Add(listBox);
                gbFilter.Location = new System.Drawing.Point(13, dy);
                gbFilter.Name = $"gbFilter{item.Id}";
                gbFilter.Size = new System.Drawing.Size(222, 217);
                gbFilter.TabIndex = 0;
                gbFilter.TabStop = false;
                gbFilter.Text = item.Name;
                gbFilter.ForeColor = Color.Red;
                gbFilter.Tag = item;
                gbFilter.Click += new EventHandler(GbFilter_Click);
                // 
                // listBox
                // 
                listBox.FormattingEnabled = true;
                listBox.Location = new System.Drawing.Point(0, 30);
                listBox.Name = $"listBox{item.Id}";
                listBox.Width = 208;
                listBox.TabIndex = 0;
                listBox.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);

                foreach (var child in item.Children)
                {
                    listBox.Items.Add(child);
                }

                gbFilter.Size = new Size(listBox.Size.Width, listBox.Size.Height + 30);
                dy += gbFilter.Size.Height + 10;
                this.Controls.Add(gbFilter);

            }
        }

        [Obsolete]
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Збираємо значення фільтрів
            List<int> values = new List<int>();
            var listGB = this.Controls.OfType<GroupBox>();
            foreach (var groupBox in listGB)
            {
                var checkedItem = groupBox.Controls.OfType<CheckedListBox>().FirstOrDefault().CheckedItems;
                foreach (var listItem in checkedItem)
                {
                    var data = listItem as FilterValueModel;
                    values.Add(data.Id);
                }
            }
            var filtersList = GetFilterNameModels();
            int[] filterValueSearchList = values.ToArray();

            var query = _context
                    .Products
                    .AsQueryable();

            foreach (var fName in filtersList)
            {
                int countFilter = 0; //Кількість співпадінь у даній групі фільтрів
                var predicate = PredicateBuilder.False<Product>();
                foreach (var fValue in fName.Children)
                {
                    for (int i = 0; i < filterValueSearchList.Length; i++)
                    {
                        var idV = fValue.Id; //id - значення фільтра
                        if (filterValueSearchList[i] == idV) //маємо співпадіння
                        {
                            predicate = predicate
                                .Or(p => p.Filters
                                    .Any(f => f.FilterValueId == idV));
                            countFilter++;
                        }
                    }
                }
                if (countFilter != 0)
                    query = query.Where(predicate);
            }

            var listProduct = query.ToList();
            dgvProducts.Rows.Clear();
            foreach (var p in listProduct)
            {
                object[] row =
                {
                    p.Id,
                    null,
                    p.Name,
                    p.Price
                };
                dgvProducts.Rows.Add(row);
            }
        }

        
        private void GbFilter_Click(object sender, EventArgs e)
        {
            var groupBox = (sender as GroupBox);
            var FilterName = groupBox.Tag as FilterNameModel;
            if (FilterName.IsCollapsed)
            {
                FilterName.IsCollapsed = false;
            }
            else
            {
                FilterName.IsCollapsed = true;

            }

            var checkedList = groupBox.Controls.OfType<CheckedListBox>().FirstOrDefault();

            checkedList.Visible = FilterName.IsCollapsed;
            var Height = FilterName.IsCollapsed == true ? checkedList.Height + 30 : 30;
            groupBox.Height = Height;

            ShowAllGroups(this.Controls.OfType<GroupBox>());

        }

        private void ShowAllGroups(IEnumerable<GroupBox> groupBoxes)
        {
            int dy = 13;
            foreach (var box in groupBoxes)
            {
                box.Location = new Point(box.Location.X, dy);
                dy += box.Size.Height + 10;
            }
        }
    }
}
