using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace UnitsConversion
{
  internal class Units : INotifyPropertyChanged
  {

    private Unit systemUnit;
    public List<Unit> UnitsList;
    private Unit userUnit;


    private Units()
    {
            UnitsList = new List<Unit>();

            loadFormulas();

    }

    public static Units Instance
    {
      get
      {
        return UnitsCreator.instance;
      }
    }

    public Unit SystemUnit
    {
      get { return systemUnit; }
      set
      {
        systemUnit = value;
      }
    }

    public List<Unit> unitsList
    {
      get { return unitsList; }
      set { unitsList = value; }
    }

    public Unit UserUnit
    {
      get { return userUnit; }
      set
      {
        userUnit = value;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public double translateFormula(string formula, double value)
        {
            int i = 0;
            char c;
            int flag = 0;
            string parsedFormula = "";
            
            while ((c = formula[i]) != ';')
            {
                i++;

                if (!Char.IsDigit(c) && Char.IsLetter(c)) {
                    flag++;
                    continue;
                }

                if (flag != 0)
                {
                    parsedFormula += value.ToString();
                    flag = 0;
                }

                parsedFormula += c;
                
            }

            return Double.Parse(new DataTable().Compute(parsedFormula, null).ToString());
        }

    internal void Changed(string name)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(name));
      }
    }

    internal List<Unit> loadFormulas()
    {
            
        using (var fs = File.OpenRead(@"C:\Users\jmill\Desktop\UnitsConversion\values.csv"))
        using (var reader = new StreamReader(fs))
        {
            string rawType, from, to, formula;
            UnitType type;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                rawType = values[0];
                from = values[1];
                to = values[2];
                formula = values[3];

                try
                {
                    type = (UnitType)Enum.Parse(typeof(UnitType), rawType);
                    UnitsList.Add(new Unit(type, from, to, formula));
                }catch(Exception parceExeption)
                {
                    continue;
                }
                }
        }
        return UnitsList;
    }

    internal double Convert(UnitType type, string from, string to, double value)
    {
      switch (type)
      {
        case UnitType.Weight:
        case UnitType.Length:
        case UnitType.Temperature:
                    foreach (var u in UnitsList)
                    {
                        if (u.Name == from && u.ToName == to) {
                            return translateFormula(u.Formula, value);
                        }
                    }

                    return 0;
                    
        default:
          throw new InvalidOperationException("Unit type not supported.");
      }
    }

    private class UnitsCreator
    {
      internal static readonly Units instance = new Units();
      static UnitsCreator() { }
    }

  }
}