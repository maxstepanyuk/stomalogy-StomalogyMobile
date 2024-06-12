

namespace StomalogyMobile;

public partial class MainPage : ContentPage
{


    public MainPage()
    {
        InitializeComponent();
        
        
        using (BaseContext context = new BaseContext())
        {
            
            List<Dentist> Dents = context.Dents.ToList();
            foreach (var p in Dents) dentistPicker.Items.Add(p.Name);
            
            dentistPicker.SelectedIndexChanged += SelectDentist;
            datePicker.DateSelected += SelectDate;

        }
    }

    internal BaseContext BaseContext
    {
        get => default;
        set
        {
        }
    }

    private void SelectDentist(object sender, System.EventArgs e)
    {
        UpdateInterface();
    }
    private void SelectDate(object sender, System.EventArgs e)
    {
        UpdateInterface();
    }

    void UpdateInterface() 
    {
        stack.Clear();
        using (BaseContext context = new BaseContext())
        {
            DateOnly data = DateOnly.FromDateTime(datePicker.Date);

            var apps = from u in context.Apps
                       where u.Date == data
                       where u.Dentist.Name == dentistPicker.SelectedItem.ToString()
                       join c in context.Pats on u.PatientId equals c.Id
                       join v in context.Cabs on u.CabinetId equals v.Id
                       join w in context.Reasons on u.ReasonId equals w.Id
                       select new
                       {
                           Id = u.Id,
                           Time = u.Time,
                           Cabinet = v.Name,
                           Tooth = u.Tooth,
                           Reason = w.Name,
                           Description = u.Description,
                           Name = c.Name,
                           Phone = c.Phone

                       };

            foreach (var app in apps)
            {
                String someText = app.Id + ", " + app.Time.ToString() + ", " + app.Cabinet.ToString() + ", " + app.Tooth.ToString() + ",\n" + app.Reason.ToString() + ",\n" + app.Description.ToString() + ",\n" + app.Name.ToString() + ",\n" + app.Phone.ToString();
                Label label = new Label { Text = someText };
                label.HorizontalTextAlignment = TextAlignment.Center;
                label.VerticalTextAlignment = TextAlignment.Center;
                BoxView boxView1 = new BoxView
                {
                    Color = Colors.Gray,
                    HeightRequest = 2,
                    Margin = new Thickness(1),
                    HorizontalOptions = LayoutOptions.Fill
                };
                BoxView boxView2 = new BoxView
                {
                    Color = Colors.Gray,
                    HeightRequest = 2,
                    Margin = 1,
                    HorizontalOptions = LayoutOptions.Fill
                };
                stack.Add(label);
                stack.Add(boxView1);
                stack.Add(boxView2);
            }
        }
    }
}

