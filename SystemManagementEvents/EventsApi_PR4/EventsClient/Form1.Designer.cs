namespace EventsClient
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            eventsGrid = new DataGridView();
            loadButton = new Button();
            cancelButton = new Button();
            addButton = new Button();
            progressBar = new ProgressBar();
            statusLabel = new Label();
            titleTextBox = new TextBox();
            datePicker = new DateTimePicker();
            roomTextBox = new TextBox();
            maxParticipantsNumeric = new NumericUpDown();
            syncLoadButton = new Button();
            searchTextBox = new TextBox();
            searchButton = new Button();
            resetSearchButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            toolTip = new ToolTip(components);
            errorProvider = new ErrorProvider(components);
            helpProvider = new HelpProvider();
            menuStrip1 = new MenuStrip();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            manualMenuItem = new ToolStripMenuItem();
            aboutMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)eventsGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxParticipantsNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // eventsGrid
            // 
            eventsGrid.AllowUserToAddRows = false;
            eventsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            eventsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            eventsGrid.Location = new Point(23, 118);
            eventsGrid.Name = "eventsGrid";
            eventsGrid.ReadOnly = true;
            eventsGrid.RowHeadersWidth = 62;
            eventsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            eventsGrid.Size = new Size(1225, 373);
            eventsGrid.TabIndex = 0;
            // 
            // loadButton
            // 
            loadButton.Location = new Point(23, 78);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(112, 34);
            loadButton.TabIndex = 1;
            loadButton.Text = "Load";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Enabled = false;
            cancelButton.Location = new Point(259, 78);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(112, 34);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // addButton
            // 
            addButton.Location = new Point(141, 78);
            addButton.Name = "addButton";
            addButton.Size = new Size(112, 34);
            addButton.TabIndex = 3;
            addButton.Text = "Add";
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += addButton_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(23, 642);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(230, 34);
            progressBar.TabIndex = 4;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(377, 83);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(70, 25);
            statusLabel.TabIndex = 5;
            statusLabel.Text = "Готово";
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(23, 524);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.PlaceholderText = "Например: День открытых дверей";
            titleTextBox.Size = new Size(230, 31);
            titleTextBox.TabIndex = 6;
            // 
            // datePicker
            // 
            datePicker.Location = new Point(377, 522);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(245, 31);
            datePicker.TabIndex = 7;
            // 
            // roomTextBox
            // 
            roomTextBox.Location = new Point(23, 582);
            roomTextBox.Name = "roomTextBox";
            roomTextBox.PlaceholderText = "Например: Аудитория 214";
            roomTextBox.Size = new Size(150, 31);
            roomTextBox.TabIndex = 8;
            // 
            // maxParticipantsNumeric
            // 
            maxParticipantsNumeric.Location = new Point(417, 590);
            maxParticipantsNumeric.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            maxParticipantsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            maxParticipantsNumeric.Name = "maxParticipantsNumeric";
            maxParticipantsNumeric.Size = new Size(205, 31);
            maxParticipantsNumeric.TabIndex = 9;
            maxParticipantsNumeric.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // syncLoadButton
            // 
            syncLoadButton.Location = new Point(965, 497);
            syncLoadButton.Name = "syncLoadButton";
            syncLoadButton.Size = new Size(283, 34);
            syncLoadButton.TabIndex = 10;
            syncLoadButton.Text = "Эксперимент с блокировкой";
            syncLoadButton.UseVisualStyleBackColor = true;
            syncLoadButton.Click += syncLoadButton_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(688, 522);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Введите название или аудитори";
            searchTextBox.Size = new Size(150, 31);
            searchTextBox.TabIndex = 11;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(706, 560);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(112, 34);
            searchButton.TabIndex = 12;
            searchButton.Text = "Find";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // resetSearchButton
            // 
            resetSearchButton.Location = new Point(706, 615);
            resetSearchButton.Name = "resetSearchButton";
            resetSearchButton.Size = new Size(112, 34);
            resetSearchButton.TabIndex = 13;
            resetSearchButton.Text = "Reset";
            resetSearchButton.UseVisualStyleBackColor = true;
            resetSearchButton.Click += resetSearchButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 496);
            label1.Name = "label1";
            label1.Size = new Size(205, 25);
            label1.TabIndex = 14;
            label1.Text = "Название мероприятия";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 554);
            label2.Name = "label2";
            label2.Size = new Size(167, 25);
            label2.TabIndex = 15;
            label2.Text = "Место проведения";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(417, 560);
            label3.Name = "label3";
            label3.Size = new Size(205, 25);
            label3.TabIndex = 16;
            label3.Text = "Количество участников";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(431, 494);
            label4.Name = "label4";
            label4.Size = new Size(164, 25);
            label4.TabIndex = 17;
            label4.Text = "Дата мероприятия";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(716, 494);
            label5.Name = "label5";
            label5.Size = new Size(89, 25);
            label5.TabIndex = 18;
            label5.Text = "Поиск по";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { справкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1249, 33);
            menuStrip1.TabIndex = 19;
            menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { manualMenuItem, aboutMenuItem });
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(97, 29);
            справкаToolStripMenuItem.Text = "Справка";
            // 
            // manualMenuItem
            // 
            manualMenuItem.Name = "manualMenuItem";
            manualMenuItem.Size = new Size(345, 34);
            manualMenuItem.Text = "Руководство пользователья";
            manualMenuItem.Click += manualMenuItem_Click;
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new Size(345, 34);
            aboutMenuItem.Text = "О программе";
            aboutMenuItem.Click += aboutMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1249, 690);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(resetSearchButton);
            Controls.Add(searchButton);
            Controls.Add(searchTextBox);
            Controls.Add(syncLoadButton);
            Controls.Add(maxParticipantsNumeric);
            Controls.Add(roomTextBox);
            Controls.Add(datePicker);
            Controls.Add(titleTextBox);
            Controls.Add(statusLabel);
            Controls.Add(progressBar);
            Controls.Add(addButton);
            Controls.Add(cancelButton);
            Controls.Add(loadButton);
            Controls.Add(eventsGrid);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)eventsGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxParticipantsNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView eventsGrid;
        private Button loadButton;
        private Button cancelButton;
        private Button addButton;
        private ProgressBar progressBar;
        private Label statusLabel;
        private TextBox titleTextBox;
        private DateTimePicker datePicker;
        private TextBox roomTextBox;
        private NumericUpDown maxParticipantsNumeric;
        private Button syncLoadButton;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button resetSearchButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ToolTip toolTip;
        private ErrorProvider errorProvider;
        private HelpProvider helpProvider;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem справкаToolStripMenuItem;
        private ToolStripMenuItem manualMenuItem;
        private ToolStripMenuItem aboutMenuItem;
    }
}
