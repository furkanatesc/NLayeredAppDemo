using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Northwind.Business.Abstract;
using Northwind.Business.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Entities.Concrete;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public Form1()
        {
            InitializeComponent();
            _productService = new ProductManager(new EfProductDal());
            _categoryService = new CategoryManager(new EfCategoryDal());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            LoadProducts();
            LoadCategories();
        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
           
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch 
            {

                
            }
        }

        

        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxProductName.Text))
            {
                dgwProduct.DataSource = _productService.GetProductsByProductName(tbxProductName.Text);
            }
            else
            {
                LoadProducts();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productService.Add(new Product
            {
                CategoryId=Convert.ToInt32(cbxCategoryId.SelectedValue),
                ProductName=tbxProductName2.Text,
                QuantityPerUnit=tbxQuantityPerUnit.Text,
                UnitPrice=Convert.ToDecimal(tbxUnitPrice)

            });
        }
    }
}
