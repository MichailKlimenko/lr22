using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace лр22
{
    public partial class Form3 : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\работа\Практика КПиЯП\лр22\лр22\BikeRentalDB.mdf;Integrated Security=True;Connect Timeout=30";
        // Строка подключения к базе данных.

        public Form3()                  // Конструктор класса Form3.
        {
            InitializeComponent();     // Инициализация компонентов формы.
        }

        private void Form3_Load(object sender, EventArgs e)  // Метод-обработчик события загрузки формы.
        {
            // Загрузка данных в таблицу Bicycles из набора данных bikeRentalDBDataSet1.
            this.bicyclesTableAdapter.Fill(this.bikeRentalDBDataSet1.Bicycles);
            // Загрузка данных в таблицу Bicycles из набора данных bikeRentalDBDataSet1.
            this.bicyclesTableAdapter.Fill(this.bikeRentalDBDataSet1.Bicycles);
            // Загрузка данных в таблицу Rentals из набора данных bikeRentalDBDataSet1.
            //this.rentalsTableAdapter.Fill(this.bikeRentalDBDataSet1.Rentals); // Отключенный код.
        }

        // Метод-обработчик события сохранения изменений в таблице Bicycles.
        private void bicyclesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();                            // Проверка данных на форме.
            this.bicyclesBindingSource.EndEdit();        // Завершение редактирования данных.
            this.tableAdapterManager.UpdateAll(this.bikeRentalDBDataSet1); // Обновление всех данных в наборе данных.
        }
    }
}
