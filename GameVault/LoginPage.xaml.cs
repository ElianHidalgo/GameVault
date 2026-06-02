using Microsoft.Maui.Storage;

namespace GameVault;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();

        chkMostrar.CheckedChanged += MostrarPassword;

        bool recordar =
            Preferences.Get("Recordar", false);

        if (recordar)
        {
            txtCorreo.Text =
                Preferences.Get("Correo", "");
        }
    }

    private void MostrarPassword(
        object sender,
        CheckedChangedEventArgs e)
    {
        txtPassword.IsPassword =
            !e.Value;
    }

    private async void Login_Clicked(
        object sender,
        EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCorreo.Text))
        {
            await DisplayAlert(
                "Error",
                "Ingrese un correo",
                "Aceptar");

            return;
        }

        if (!txtCorreo.Text.Contains("@"))
        {
            await DisplayAlert(
                "Error",
                "Correo inválido",
                "Aceptar");

            return;
        }

        if (txtCorreo.Text ==
           "admin@gamevault.com"
           &&
           txtPassword.Text == "1234")
        {
            if (chkRecordar.IsChecked)
            {
                Preferences.Set(
                    "Correo",
                    txtCorreo.Text);

                Preferences.Set(
                    "Recordar",
                    true);
            }

            await Navigation.PushAsync(
                new MainPage());
        }
        else
        {
            await DisplayAlert(
                "Error",
                "Credenciales incorrectas",
                "Aceptar");
        }
    }
}