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
    public partial class Form2 : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\работа\Практика КПиЯП\лр22\лр22\BikeRentalDB.mdf;Integrated Security=True;Connect Timeout=30";
        // Строка подключения к базе данных.

        public Form2()                  // Конструктор класса Form2.
        {
            InitializeComponent();     // Инициализация компонентов формы.
        }

        private void Form2_Load(object sender, EventArgs e)  // Метод-обработчик события загрузки формы.
        {
            // Загрузка данных в таблицу Clients из набора данных bikeRentalDBDataSet1.
            this.clientsTableAdapter.Fill(this.bikeRentalDBDataSet1.Clients);
            // Загрузка данных в таблицу Clients из набора данных bikeRentalDBDataSet1.
            this.clientsTableAdapter.Fill(this.bikeRentalDBDataSet1.Clients);
        }

        // Метод-обработчик события сохранения изменений в таблице Clients.
        private void clientsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();                            // Проверка данных на форме.
            this.clientsBindingSource.EndEdit();        // Завершение редактирования данных.
            this.tableAdapterManager.UpdateAll(this.bikeRentalDBDataSet1); // Обновление всех данных в наборе данных.
        }
    }
}
