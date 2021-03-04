using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
        }

      

        private void Query_button_Click(object sender, RoutedEventArgs e)
        {
            if (GetSpec.IsChecked == true)
            {
                foreach (DataGridTextColumn i in Table.Columns)
                {
                    i.Visibility = Visibility.Hidden;

                }
                if (FormatMatch(ID_Textbox.Text))
                {
                    using (UMESContext UMES = new UMESContext())
                    {
                        var specList = UMES.Knu.Where(p => p.Id == uint.Parse(ID_Textbox.Text)).ToList();

                        foreach (DataGridTextColumn i in Table.Columns)
                        {
                            i.Visibility = Visibility.Visible;
                            if (i.DisplayIndex >= 3)
                            {
                                break;
                            }
                        }
                        Table.ItemsSource = specList;
                    }
                }
                else if (ID_Textbox.Text == "")
                {
                    using (UMESContext UMES = new UMESContext())
                    {
                        var specList = UMES.Knu.ToList();

                        foreach (DataGridTextColumn i in Table.Columns)
                        {
                            i.Visibility = Visibility.Visible;
                            if (i.DisplayIndex >= 3)
                            {
                                break;
                            }
                        }
                        Table.ItemsSource = specList;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid enter, please try again");
                }
            }
            else if (AddSpec.IsChecked == true)
            {
                List<string> textboxToString = new List<string>();

                textboxToString.Add(ID_Textbox.Text);
                textboxToString.Add(NameOfSpec_Textbox.Text);
                textboxToString.Add(SpecOfFac_Textbox.Text);

                if (FormatMatch(textboxToString))
                {
                    SpecializationOfFaculties addSpec = new SpecializationOfFaculties();

                    addSpec.Id = int.Parse(textboxToString[0]);
                    addSpec.NameOfSpecialization = textboxToString[1];
                    addSpec.SpecializationOfFaculty = textboxToString[2];

                    using (UMESContext UMES = new UMESContext())
                    {
                        UMES.Knu.Add(addSpec);
                        UMES.SaveChanges();
                        MessageBox.Show("1 specialization was added");
                    }
                }
            }
            else if (ChangeSpec.IsChecked == true)
            {
                List<string> textboxToString = new List<string>();

                textboxToString.Add(ID_Textbox.Text);
                textboxToString.Add(NameOfSpec_Textbox.Text);
                textboxToString.Add(SpecOfFac_Textbox.Text);

                foreach (var i in textboxToString)
                {
                    if (!(FormatMatch(i) || i == ""))
                    {
                        textboxToString = null;
                        break;
                    }
                }
                if (ID_Textbox.Text != "")
                {
                    if (textboxToString != null)
                    {
                        using (UMESContext UMES = new UMESContext())
                        {
                            SpecializationOfFaculties changedSpec = new SpecializationOfFaculties();

                            var specForChange = UMES.Knu.Where(p => p.Id == int.Parse(textboxToString[0])).ToList()[0];

                            changedSpec.Id = int.Parse(textboxToString[0]);
                            if (textboxToString[1] != "")
                            {
                                changedSpec.NameOfSpecialization = textboxToString[1];
                            }
                            else
                            {
                                changedSpec.NameOfSpecialization = specForChange.NameOfSpecialization;
                            }
                            if (textboxToString[2] != "")
                            {
                                changedSpec.SpecializationOfFaculty = textboxToString[2];
                            }
                            else
                            {
                                changedSpec.SpecializationOfFaculty = specForChange.SpecializationOfFaculty;
                            }

                            UMES.Knu.Remove(specForChange);
                            UMES.Knu.Add(changedSpec);
                            UMES.SaveChanges();

                            MessageBox.Show("1 specialization was chenged");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid enter in some of teaxtbox");
                    }
                }
                else
                {
                    MessageBox.Show("Enter ID for changing specialization");
                }
            }
            else if (DelSpec.IsChecked == true)
            {
                if (FormatMatch(ID_Textbox.Text))
                {
                    using (UMESContext UMES = new UMESContext())
                    {
                        var removeSpec = UMES.Knu.Where(p => p.Id == int.Parse(ID_Textbox.Text)).ToList();

                        UMES.Knu.Remove(removeSpec[0]);
                        UMES.SaveChanges();
                    }
                    MessageBox.Show("1 specialization was deleted");
                }
                else
                {
                    MessageBox.Show("Invalid enter ID, please try again");
                }
            }
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void GetSpec_Checked(object sender, RoutedEventArgs e)
        {
            ID_Textbox.Text = null;
            NameOfSpec_Textbox.Text = null;
            SpecOfFac_Textbox.Text = null;
            NameOfSpec_Textbox.IsEnabled = false;
            SpecOfFac_Textbox.IsEnabled = false;
        }

        private void AddSpec_Checked(object sender, RoutedEventArgs e)
        {
            ID_Textbox.Text = null;
            NameOfSpec_Textbox.Text = null;
            SpecOfFac_Textbox.Text = null;
            NameOfSpec_Textbox.IsEnabled = true;
            SpecOfFac_Textbox.IsEnabled = true;
        }
        private void ChangeSpec_Checked(object sender, RoutedEventArgs e)
        {
            ID_Textbox.Text = null;
            NameOfSpec_Textbox.Text = null;
            SpecOfFac_Textbox.Text = null;
            NameOfSpec_Textbox.IsEnabled = true;
            SpecOfFac_Textbox.IsEnabled = true;
        }
        private void DelSpec_Checked(object sender, RoutedEventArgs e)
        {
            ID_Textbox.Text = null;
            NameOfSpec_Textbox.Text = null;
            SpecOfFac_Textbox.Text = null;
            NameOfSpec_Textbox.IsEnabled = false;
            SpecOfFac_Textbox.IsEnabled = false;
        }
        public bool FormatMatch(List<string> textboxToString)
        {
            Regex checkReg = new Regex(@"^[a-zA-z]|[а-яёА-ЯЁ]$");
            int helpX = 0;

            foreach (var i in textboxToString)
            {
                if (!(int.TryParse(i, out helpX) || checkReg.Match(i).Success))
                {
                    return false;
                }
            }

            return true;
        }
        public bool FormatMatch(string textboxToString)
        {
            Regex checkReg = new Regex(@"^[a-zA-z]|[а-яёА-ЯЁ]$");
            int helpX = 0;

            if (!(int.TryParse(textboxToString, out helpX) || checkReg.Match(textboxToString).Success))
            {
                return false;
            }
            return true;
        }

        
    }
}
