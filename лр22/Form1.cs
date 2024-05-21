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
using System.IO;

namespace лр22
{
    public partial class Form1 : Form
    {
        private string dbFileName = "BikeRentalDB.mdf";  // Имя файла базы данных.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\работа\Практика КПиЯП\лр22\лр22\BikeRentalDB.mdf;Integrated Security=True;Connect Timeout=30";
        // Строка подключения к базе данных.
        private string dbPath;                          // Путь к файлу базы данных.
        private string dbConnectionString;              // Строка подключения к базе данных с включенным путем к файлу.

        public Form1()                                  // Конструктор класса Form1.
        {
            InitializeComponent();                     // Инициализация компонентов формы.
            InitializeDatabase();                      // Вызов метода инициализации базы данных.
        }

        private void InitializeDatabase()              // Метод для инициализации базы данных.
        {
            dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);  // Получение полного пути к файлу базы данных.
            dbConnectionString = connectionString + $";AttachDbFilename={dbPath};";    // Формирование строки подключения с путем к базе данных.

            if (!File.Exists(dbPath))                 // Проверка существования файла базы данных.
            {
                CreateDatabase();                     // Вызов метода для создания базы данных, если файл не существует.
            }
        }

        private void CreateDatabase()                 // Метод для создания базы данных.
        {
            try
            {
                // SQL-команда для создания базы данных.
                var createDbCommand = $@"
                CREATE DATABASE [BikeRentalDB]
                ON (NAME = N'BikeRentalDB', FILENAME = '{dbPath}')";

                using (var connection = new SqlConnection(connectionString)) // Создание подключения к SQL Server.
                {
                    connection.Open();                    // Открытие подключения.
                    using (var command = new SqlCommand(createDbCommand, connection)) // Создание команды SQL.
                    {
                        command.ExecuteNonQuery();        // Выполнение команды SQL для создания базы данных.
                    }
                }

                // SQL-команды для создания таблиц в базе данных.
                var createTablesCommand = @"
                USE [BikeRentalDB];
                CREATE TABLE [dbo].[Clients] (
                    [ID]          INT           IDENTITY (1, 1) NOT NULL,
                    [FirstName]   NVARCHAR (50) NULL,
                    [LastName]    NVARCHAR (50) NULL,
                    [PhoneNumber] NVARCHAR (20) NULL,
                    PRIMARY KEY CLUSTERED ([ID] ASC)
                );
                CREATE TABLE [dbo].[Bicycles] (
                    [ID]                INT             IDENTITY (1, 1) NOT NULL,
                    [Model]             NVARCHAR (100)  NULL,
                    [Type]              NVARCHAR (50)   NULL,
                    [FrameSize]         NVARCHAR (20)   NULL,
                    [RentalCostPerHour] DECIMAL (10, 2) NULL,
                    [Photo]             NVARCHAR (MAX)  NULL,
                    PRIMARY KEY CLUSTERED ([ID] ASC)
                );
                CREATE TABLE [dbo].[Rentals] (
                    [ID]        INT             IDENTITY (1, 1) NOT NULL,
                    [BicycleID] INT             NULL,
                    [ClientID]  INT             NULL,
                    [StartTime] DATETIME        NULL,
                    [EndTime]   DATETIME        NULL,
                    [Cost]      DECIMAL (10, 2) NULL,
                    PRIMARY KEY CLUSTERED ([ID] ASC),
                    FOREIGN KEY ([BicycleID]) REFERENCES [dbo].[Bicycles] ([ID]),
                    FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Clients] ([ID])
                );";

                using (var connection = new SqlConnection(dbConnectionString)) // Создание нового подключения с включенным путем к файлу базы данных.
                {
                    connection.Open();                    // Открытие подключения.
                    using (var command = new SqlCommand(createTablesCommand, connection)) // Создание команды SQL для создания таблиц.
                    {
                        command.ExecuteNonQuery();        // Выполнение команды SQL для создания таблиц.
                    }
                }

                MessageBox.Show("База данных и таблицы успешно созданы!"); // Сообщение об успешном создании базы данных и таблиц.
            }
            catch (Exception ex)                          // Обработка исключений.
            {
                MessageBox.Show($"Произошла ошибка при создании базы данных: {ex.Message}"); // Сообщение об ошибке при создании базы данных.
            }
        }

        // Метод-обработчик события сохранения изменений в таблице Rentals.
        private void rentalsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();                              // Проверка данных на форме.
            this.rentalsBindingSource.EndEdit();          // Завершение редактирования данных.
            this.tableAdapterManager.UpdateAll(this.bikeRentalDBDataSet1); // Обновление всех данных в наборе данных.
        }

        // Метод-обработчик события загрузки формы.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Загрузка данных в таблицу Rentals из набора данных bikeRentalDBDataSet1.
            this.rentalsTableAdapter.Fill(this.bikeRentalDBDataSet1.Rentals);
        }

        // Метод-обработчик события клика по элементу меню "Клиенты".
        private void клиентыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();                    // Создание новой формы Form2.
            form2.ShowDialog();                           // Показ формы в модальном режиме.
        }

        // Метод-обработчик события клика по элементу меню "Велосипеды".
        private void велосипедыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();                    // Создание новой формы Form3.
            form3.ShowDialog();                           // Показ формы в модальном режиме.
        }
    }
}
