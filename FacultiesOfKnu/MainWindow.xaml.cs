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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (UMESContext UMES = new UMESContext())
            {
                if (UMES.Knu.ToList().Count == 0)
                {
                    UMES.Knu.Add(new SpecializationOfFaculties { Id = 1, NameOfSpecialization = "Радіотехніка", SpecializationOfFaculty = "Радіотехніка, електронні пристрої" });
                    UMES.Knu.Add(new SpecializationOfFaculties { Id = 2, NameOfSpecialization = "Кібернетика", SpecializationOfFaculty = "Кібернетика, комп'ютерні мережі" });
                }
                else if (UMES.Knu.ToList().Count == 1)
                {
                    UMES.Knu.Add(new SpecializationOfFaculties { Id = 2, NameOfSpecialization = "Кібернетика", SpecializationOfFaculty = "Кібернетика, комп'ютерні мережі" });
                }
                UMES.SaveChanges();

                var k = UMES.FacultyOfKnu.ToList();
                if (UMES.FacultyOfKnu.ToList().Count == 0)
                {
                    UMES.FacultyOfKnu.Add(new FacultyOfKnu { Id = 1, NameOfFaculty = "ФРЕКС", AdvancedPlacement = "фізика", NumberOfFacultyMembers = 1594, NumberOfFacultyStudents = 4837, IdS = 1 });
                    UMES.FacultyOfKnu.Add(new FacultyOfKnu { Id = 2, NameOfFaculty = "ФКНК", AdvancedPlacement = "програмування", NumberOfFacultyMembers = 1154, NumberOfFacultyStudents = 3592, IdS = 2 });
                }
                else if (UMES.FacultyOfKnu.ToList().Count == 1)
                {
                    UMES.FacultyOfKnu.Add(new FacultyOfKnu { Id = 2, NameOfFaculty = "ФКНК", AdvancedPlacement = "програмування", NumberOfFacultyMembers = 1154, NumberOfFacultyStudents = 3592, IdS = 2 });
                }
                UMES.SaveChanges();

                var listOfSpec = UMES.Knu.Select(p => p.NameOfSpecialization).ToList();
                ID_OfSpec_Combobox.ItemsSource = listOfSpec;
            }
        }

    private void Spec_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (GetFac.IsChecked == true)
            {
                ID_OfSpec_Combobox.IsEnabled = true;
            }
            if (AddFac.IsChecked == true)
            {
                ID_CheckBox.IsEnabled = false;
                ID_Textbox.IsEnabled = false;
                FacultyName_TextBox.IsEnabled = true;
                MainSubj_TextBox.IsEnabled = true;
                NumbOfLect_Textbox.IsEnabled = true;
                NumbOfStud_TextBox.IsEnabled = true;
                ID_OfSpec_Combobox.IsEnabled = true;

            }
            if (DelFac.IsChecked == true)
            {
                ID_CheckBox.IsEnabled = false;
                ID_Textbox.IsEnabled = false;
                ID_OfSpec_Combobox.IsEnabled = true;
            }
        }
        private void Spec_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (GetFac.IsChecked == true)
            {
                ID_OfSpec_Combobox.SelectedItem = null;
                ID_OfSpec_Combobox.IsEnabled = false;
            }
            if (AddFac.IsChecked == true)
            {
                ID_CheckBox.IsEnabled = true;
                ID_Textbox.IsEnabled = false;
            }
            if (ChangeFac.IsChecked == true)
            {
                ID_Textbox.IsEnabled = true;
            }
            if (DelFac.IsChecked == true)
            {
                ID_CheckBox.IsEnabled = true;
                ID_Textbox.IsEnabled = true;
            }
        }

        private void ID_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (GetFac.IsChecked == true)
            {
                ID_Textbox.IsEnabled = true;
            }
            if (AddFac.IsChecked == true)
            {
                Spec_CheckBox.IsEnabled = false;

                ID_Textbox.IsEnabled = true;
                FacultyName_TextBox.IsEnabled = true;
                MainSubj_TextBox.IsEnabled = true;
                NumbOfLect_Textbox.IsEnabled = true;
                NumbOfStud_TextBox.IsEnabled = true;
                ID_OfSpec_Combobox.IsEnabled = true;
            }
            if (ChangeFac.IsChecked == true)
            {
                ID_Textbox.IsEnabled = true;
                FacultyName_TextBox.IsEnabled = true;
                MainSubj_TextBox.IsEnabled = true;
                NumbOfLect_Textbox.IsEnabled = true;
                NumbOfStud_TextBox.IsEnabled = true;
                ID_OfSpec_Combobox.IsEnabled = true;
            }
            if (DelFac.IsChecked == true)
            {
                ID_Textbox.IsEnabled = true;
                Spec_CheckBox.IsEnabled = false;
                ID_OfSpec_Combobox.IsEnabled = false;
            }
        }

        private void ID_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (GetFac.IsChecked == true)
            {
                ID_Textbox.IsEnabled = false;
                ID_Textbox.Text = null;
            }
            if (AddFac.IsChecked == true)
            {
                Spec_CheckBox.IsEnabled = true;
            }
            if (DelFac.IsChecked == true)
            {
                Spec_CheckBox.IsEnabled = true;
                ID_OfSpec_Combobox.IsEnabled = true;
            }
        }

        private void Query_Button_Click(object sender, RoutedEventArgs e)
        {
            if (GetFac.IsChecked == true)
            {
                foreach (DataGridTextColumn i in Table.Columns)
                {
                    i.Visibility = Visibility.Hidden;

                }
                if (ID_CheckBox.IsChecked == true && Spec_CheckBox.IsChecked == false)
                {
                    if (FormatMatch(ID_Textbox.Text))
                    {
                        using (UMESContext UMES = new UMESContext())
                        {
                            var facultyList = UMES.FacultyOfKnu.Where(p => p.Id == uint.Parse(ID_Textbox.Text)).ToList();

                            foreach (DataGridTextColumn i in Table.Columns)
                            {
                                i.Visibility = Visibility.Visible;
                                if (i.DisplayIndex >= 4)
                                {
                                    break;
                                }
                            }
                            Table.ItemsSource = facultyList;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid enter, please try again");
                    }
                }
                else if (ID_CheckBox.IsChecked == false && Spec_CheckBox.IsChecked == true)
                {
                    using (UMESContext UMES = new UMESContext())
                    {
                        var facultyList = UMES.FacultyOfKnu.Join(UMES.Knu,
                            p => p.IdS,
                            c => c.Id,
                            (p, c) => new
                            {
                                p.Id,
                                p.NameOfFaculty,
                                p.AdvancedPlacement,
                                p.NumberOfFacultyMembers,
                                p.NumberOfFacultyStudents,
                                c.NameOfSpecialization
                            });

                        foreach (DataGridTextColumn i in Table.Columns)
                        {
                            i.Visibility = Visibility.Visible;
                            if (i.DisplayIndex >= 5)
                            {
                                break;
                            }
                        }

                        Table.ItemsSource = facultyList.Where(p => p.NameOfSpecialization == ID_OfSpec_Combobox.SelectedItem.ToString()).ToList();
                    }
                }
                else if (ID_CheckBox.IsChecked == true && Spec_CheckBox.IsChecked == true)
                {
                    if (FormatMatch(ID_Textbox.Text))
                    {
                        using (UMESContext UMES = new UMESContext())
                        {
                            var facultyList = UMES.FacultyOfKnu.Join(UMES.Knu,
                           p => p.IdS,
                           c => c.Id,
                           (p, c) => new
                           {
                               p.Id,
                               p.NameOfFaculty,
                               p.AdvancedPlacement,
                               p.NumberOfFacultyMembers,
                               p.NumberOfFacultyStudents,
                               c.NameOfSpecialization
                           });

                            foreach (DataGridTextColumn i in Table.Columns)
                            {
                                i.Visibility = Visibility.Visible;
                                if (i.DisplayIndex >= 5)
                                {
                                    break;
                                }
                            }
                            Table.ItemsSource = facultyList.Where(p => p.Id == byte.Parse(ID_Textbox.Text)).Where(p => p.NameOfSpecialization == ID_OfSpec_Combobox.SelectedItem.ToString()).ToList();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid ID enter, please try again");
                    }
                }
                else
                {
                    using (UMESContext UMES = new UMESContext())
                    {
                        var facultyList = UMES.FacultyOfKnu.Join(UMES.Knu,
                            p => p.IdS,
                            c => c.Id,
                            (p, c) => new
                            {
                                p.Id,
                                p.AdvancedPlacement,
                                p.NameOfFaculty,
                                p.NumberOfFacultyMembers,
                                p.NumberOfFacultyStudents,
                                c.NameOfSpecialization,
                                c.SpecializationOfFaculty
                            });

                        foreach (DataGridTextColumn i in Table.Columns)
                        {
                            i.Visibility = Visibility.Visible;
                            if (i.DisplayIndex >= 6)
                            {
                                break;
                            }
                        }

                        Table.ItemsSource = facultyList.ToList();
                    }
                }
            }
            else if (AddFac.IsChecked == true)
            {
                if (ID_CheckBox.IsChecked == true)
                {
                    int helpX = 0;
                    List<string> textboxToString = new List<string>();

                    textboxToString.Add(ID_Textbox.Text);
                    textboxToString.Add(FacultyName_TextBox.Text);
                    textboxToString.Add(MainSubj_TextBox.Text);
                    textboxToString.Add(NumbOfLect_Textbox.Text);
                    textboxToString.Add(NumbOfStud_TextBox.Text);
                    textboxToString.Add(ID_OfSpec_Combobox.SelectedItem.ToString());

                    if (FormatMatch(textboxToString))
                    {
                        FacultyOfKnu addNewFaculty = new FacultyOfKnu();

                        addNewFaculty.Id = int.Parse(textboxToString[0]);
                        addNewFaculty.NameOfFaculty = textboxToString[1];
                        addNewFaculty.AdvancedPlacement = textboxToString[2];
                        addNewFaculty.NumberOfFacultyMembers = int.Parse(textboxToString[3]);
                        addNewFaculty.NumberOfFacultyStudents = int.Parse(textboxToString[4]);

                        using (UMESContext UMES = new UMESContext())
                        {
                            helpX = (UMES.Knu.Where(p => p.NameOfSpecialization == ID_OfSpec_Combobox.SelectedItem.ToString()).ToList())[0].Id;
                            addNewFaculty.IdS = helpX;

                            UMES.FacultyOfKnu.Add(addNewFaculty);
                            UMES.SaveChanges();
                        }
                        MessageBox.Show("You add 1 new faculty");
                    }
                    else
                    {
                        MessageBox.Show("Invalid enter, please try again");
                    }
                }
                else if (Spec_CheckBox.IsChecked == true)
                {
                    List<string> textboxToString = new List<string>();

                    textboxToString.Add(FacultyName_TextBox.Text);
                    textboxToString.Add(MainSubj_TextBox.Text);
                    textboxToString.Add(NumbOfLect_Textbox.Text);
                    textboxToString.Add(NumbOfStud_TextBox.Text);
                    textboxToString.Add(ID_OfSpec_Combobox.SelectedItem.ToString());

                    if (FormatMatch(textboxToString))
                    {
                        FacultyOfKnu addNewFaculty = new FacultyOfKnu();

                        addNewFaculty.NameOfFaculty = textboxToString[0];
                        addNewFaculty.AdvancedPlacement = textboxToString[1];
                        addNewFaculty.NumberOfFacultyMembers = int.Parse(textboxToString[2]);
                        addNewFaculty.NumberOfFacultyStudents = int.Parse(textboxToString[3]);

                        using (UMESContext UMES = new UMESContext())
                        {
                            int helpX = 0;
                            int IdS = NameOfSpecToIdS(textboxToString[4]);
                            addNewFaculty.IdS = IdS;

                            var id = UMES.FacultyOfKnu.ToList();
                            helpX = id.Last().Id;

                            addNewFaculty.Id = ++helpX;

                            UMES.FacultyOfKnu.Add(addNewFaculty);
                            UMES.SaveChanges();
                        }
                        MessageBox.Show("You add 1 new faculty");
                    }
                    else
                    {
                        MessageBox.Show("Invalid enter in some of teaxtbox");
                    }
                }
            }
            else if (ChangeFac.IsChecked == true)
            {
                if (ID_CheckBox.IsChecked == true)
                {
                    int helpX = 0;
                    List<string> textboxToString = new List<string>();

                    textboxToString.Add(ID_Textbox.Text);
                    textboxToString.Add(FacultyName_TextBox.Text);
                    textboxToString.Add(MainSubj_TextBox.Text);
                    textboxToString.Add(NumbOfLect_Textbox.Text);
                    textboxToString.Add(NumbOfStud_TextBox.Text);
                    
                    foreach (var i in textboxToString)
                    {
                        if (!(FormatMatch(i) || i == ""))
                        {
                            textboxToString = null;
                            break;
                        }
                    }
                    if (textboxToString != null )
                    {
                        using (UMESContext UMES = new UMESContext())
                        {
                            if (int.TryParse(ID_Textbox.Text, out helpX))
                            {
                                FacultyOfKnu changeDataOfFaculty = new FacultyOfKnu();

                                var facForChange = (UMES.FacultyOfKnu.Where(p => p.Id == int.Parse(textboxToString[0])).ToList())[0];

                                changeDataOfFaculty.Id = int.Parse(textboxToString[0]);
                                if (textboxToString[1] != "")
                                {
                                    changeDataOfFaculty.NameOfFaculty = textboxToString[1];
                                }
                                else
                                {
                                    changeDataOfFaculty.NameOfFaculty = facForChange.NameOfFaculty;
                                }
                                if (textboxToString[2] != "")
                                {
                                    changeDataOfFaculty.AdvancedPlacement = textboxToString[2];
                                }
                                else
                                {
                                    changeDataOfFaculty.AdvancedPlacement = facForChange.AdvancedPlacement;
                                }
                                if (textboxToString[3] != "")
                                {
                                    changeDataOfFaculty.NumberOfFacultyMembers = int.Parse(textboxToString[3]);
                                }
                                else
                                {
                                    changeDataOfFaculty.NumberOfFacultyMembers = facForChange.NumberOfFacultyMembers;
                                }
                                if (textboxToString[4] != "")
                                {
                                    changeDataOfFaculty.NumberOfFacultyStudents = int.Parse(textboxToString[3]);
                                }
                                else
                                {
                                    changeDataOfFaculty.NumberOfFacultyStudents = facForChange.NumberOfFacultyStudents;
                                }
                                if (ID_OfSpec_Combobox.SelectedItem != null)
                                {
                                    changeDataOfFaculty.IdS = NameOfSpecToIdS(ID_OfSpec_Combobox.SelectedItem.ToString());
                                }
                                else
                                {
                                    changeDataOfFaculty.IdS = facForChange.IdS;
                                }

                                UMES.FacultyOfKnu.Remove(facForChange);
                                UMES.FacultyOfKnu.Add(changeDataOfFaculty);
                                UMES.SaveChanges();
                                MessageBox.Show("Faculty indormation was changed");
                            }
                            else
                            {
                                MessageBox.Show("Invalid ID enter ");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid enter in some of teaxtbox");
                    }

                }
                else
                {
                    MessageBox.Show("Please enter ID_Checkbox");
                }
            }
            else if (DelFac.IsChecked == true)
            {
                if (ID_CheckBox.IsChecked == true)
                {
                    if (FormatMatch(ID_Textbox.Text))
                    {
                        using (UMESContext UMES = new UMESContext())
                        {
                            var removeFaculty = UMES.FacultyOfKnu.Where(p => p.Id == int.Parse(ID_Textbox.Text)).ToList();

                            UMES.FacultyOfKnu.Remove(removeFaculty[0]);
                            UMES.SaveChanges();
                        }
                        MessageBox.Show("You delete 1 faculty ");
                    }
                    else
                    {
                        MessageBox.Show("Invalid enter ID, please try again");
                    }
                }
                else if (Spec_CheckBox.IsChecked == true)
                {
                    if (ID_OfSpec_Combobox.SelectedItem != null)
                    {
                        using (UMESContext UMES = new UMESContext())
                        {
                            int idS = NameOfSpecToIdS(ID_OfSpec_Combobox.SelectedItem.ToString());

                            foreach (var i in UMES.FacultyOfKnu.Where(p => p.IdS == idS).ToList())
                            {
                                UMES.FacultyOfKnu.Remove(i);
                            }
                            UMES.SaveChanges();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please, enter \"Спеціалізація факультета\"");
                    }
                }
            }
        }
        private void DirectionOfActivity_Button_Click(object sender, RoutedEventArgs e)
        {
            
            SecondWindow SpecWindow = new SecondWindow();
            SpecWindow.Show();
            Close();
        }
        private void GetFac_Checked(object sender, RoutedEventArgs e)
        {
            Spec_CheckBox.IsEnabled = true;
            ID_CheckBox.IsEnabled = true;

            Spec_CheckBox.IsChecked = false;
            ID_CheckBox.IsChecked = false;

            //Textboxes
            ID_Textbox.IsEnabled = false;
            FacultyName_TextBox.IsEnabled = false;
            MainSubj_TextBox.IsEnabled = false;
            NumbOfLect_Textbox.IsEnabled = false;
            NumbOfStud_TextBox.IsEnabled = false;
            ID_OfSpec_Combobox.IsEnabled = false;
        }
        private void GetFac_Unchecked(object sender, RoutedEventArgs e)
        {
            //Textboxes
            ID_Textbox.IsEnabled = true;
            FacultyName_TextBox.IsEnabled = true;
            MainSubj_TextBox.IsEnabled = true;
            NumbOfLect_Textbox.IsEnabled = true;
            NumbOfStud_TextBox.IsEnabled = true;
            ID_OfSpec_Combobox.IsEnabled = true;
        }

        private void AddFac_Checked(object sender, RoutedEventArgs e)
        {
            Spec_CheckBox.IsEnabled = true;
            ID_CheckBox.IsEnabled = true;

            Spec_CheckBox.IsChecked = false;
            ID_CheckBox.IsChecked = false;

            if (ID_CheckBox.IsChecked != true || Spec_CheckBox.IsChecked != true)
            {
                ID_Textbox.IsEnabled = false;
                FacultyName_TextBox.IsEnabled = false;
                MainSubj_TextBox.IsEnabled = false;
                NumbOfLect_Textbox.IsEnabled = false;
                NumbOfStud_TextBox.IsEnabled = false;
                ID_OfSpec_Combobox.IsEnabled = false;
            }
        }


        private void ChacgeFac_Checked(object sender, RoutedEventArgs e)
        {
            Spec_CheckBox.IsEnabled = false;
            ID_CheckBox.IsEnabled = true;

            Spec_CheckBox.IsChecked = false;
            ID_CheckBox.IsChecked = false;
            MessageBox.Show("Enter ID of faculty and all infomation that you wanna change");
        }
        
        private void DeleteFac_Checked(object sender, RoutedEventArgs e)
        {
            Spec_CheckBox.IsEnabled = true;
            ID_CheckBox.IsEnabled = true;

            Spec_CheckBox.IsChecked = false;
            ID_CheckBox.IsChecked = false;

            //Textboxes
            FacultyName_TextBox.IsEnabled = false;
            MainSubj_TextBox.IsEnabled = false;
            NumbOfLect_Textbox.IsEnabled = false;
            NumbOfStud_TextBox.IsEnabled = false;
            ID_OfSpec_Combobox.IsEnabled = false;
        }

        private void DeleteFac_Unchecked(object sender, RoutedEventArgs e)
        {
            //Textboxes
            FacultyName_TextBox.IsEnabled = true;
            MainSubj_TextBox.IsEnabled = true;
            NumbOfLect_Textbox.IsEnabled = true;
            NumbOfStud_TextBox.IsEnabled = true;
            ID_OfSpec_Combobox.IsEnabled = true;
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

            if (!(int.TryParse(textboxToString, out helpX)  || checkReg.Match(textboxToString).Success))
            {
                return false;
            }
            return true;
        }
        
        public int NameOfSpecToIdS(string nameOfFaculty)
        {
            int IdS = 0;

            using (UMESContext UMES = new UMESContext())
            {
                IdS = UMES.Knu.Where(p => p.NameOfSpecialization == nameOfFaculty).ToList()[0].Id;
            }
            return IdS;
        }
        public string IdSToNameOfSpec(int idS)
        {
            string nameOfFaculty;

            using (UMESContext UMES = new UMESContext())
            {
                nameOfFaculty = UMES.Knu.Where(p => p.Id == idS).ToList()[0].NameOfSpecialization;
            }
            return nameOfFaculty;
        }

        
    }   
}
