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

namespace UnitsConversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string input;
        private double x;
        private List<Unit> unitList;
        private List<UnitType> typeList;
        private List<string> primaryUnitList;
        private List<string> secondaryUnitList;

        public MainWindow()
        {
            InitializeComponent();

            unitList = Units.Instance.loadFormulas();
            typeList = unitList.Select(x => x.Type).Distinct().ToList();


            foreach (var x in typeList)
            {
                typeBox.Items.Add(x.ToString());
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            input = quantity.Text;
            string type = typeBox.SelectedItem.ToString();
            string primary = prUnits.SelectedItem.ToString();
            string secondary = secUnits.SelectedItem.ToString();

            try
            {
                UnitType t = (UnitType)Enum.Parse(typeof(UnitType), type);
                x = Units.Instance.Convert(t, primary.ToString(), secondary.ToString(), Double.Parse(input));
            }
            catch (Exception ex) {
                Console.Write(ex);
            }

            result.Text = x.ToString();

        }

        private void typeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            primaryUnitList = unitList.Select(x => x.Name).Distinct().ToList();

            foreach (var x in primaryUnitList)
            {
                prUnits.Items.Add(x.ToString());
            }
        }

        private void prUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            secondaryUnitList = unitList.Select(x => x.ToName).Distinct().ToList();

            foreach (var x in secondaryUnitList)
            {
                secUnits.Items.Add(x.ToString());
            }
        }

        private void secUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
