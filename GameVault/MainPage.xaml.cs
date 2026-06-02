using GameVault.Models;
using System.Collections.ObjectModel;

namespace GameVault;

public partial class MainPage : ContentPage
{
    ObservableCollection<Activo> activos = new();
    ObservableCollection<Activo> activosFiltrados = new();

    public MainPage()
    {
        InitializeComponent();

        cvActivos.ItemsSource = activosFiltrados;
        pkFiltroCategoria.SelectedIndex = 0;
    }

    private void AgregarActivo(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
            return;

        Activo nuevo = new Activo
        {
            Nombre = txtNombre.Text,
            Plataforma = txtPlataforma.Text,
            Categoria = pkCategoria.SelectedItem?.ToString(),
            Estado = pkEstado.SelectedItem?.ToString()
        };

        activos.Add(nuevo);
        ActualizarCatalogo();
        Limpiar();
    }

    private void LimpiarCampos(object sender, EventArgs e)
    {
        Limpiar();
    }

    private void Limpiar()
    {
        txtNombre.Text = "";
        txtPlataforma.Text = "";
        pkCategoria.SelectedIndex = -1;
        pkEstado.SelectedIndex = -1;
    }

    private void BuscarActivo(object sender, TextChangedEventArgs e)
    {
        ActualizarCatalogo();
    }

    private void FiltrarCategoria(object sender, EventArgs e)
    {
        ActualizarCatalogo();
    }

    private void ActualizarCatalogo()
    {
        activosFiltrados.Clear();

        string texto = sbBuscar.Text?.ToLower() ?? "";
        string categoria = pkFiltroCategoria.SelectedItem?.ToString() ?? "Todos";

        foreach (var a in activos)
        {
            bool coincideTexto = a.Nombre.ToLower().Contains(texto);
            bool coincideCategoria = categoria == "Todos" || a.Categoria == categoria;

            if (coincideTexto && coincideCategoria)
                activosFiltrados.Add(a);
        }
    }
}