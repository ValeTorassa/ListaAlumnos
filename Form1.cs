using System;
using System.Windows.Forms;

namespace ListaAlumnos
{
    public partial class listaAlumnos : Form
    {
        // Constructor de la clase listaAlumnos
        public listaAlumnos()
        {
            // Inicialización de componentes, definición de valores máximos de dtpNacimiento y dtpIngreso
            // y vaciado de campos de texto lblError y lblEliminar
            InitializeComponent();
            dtpNacimiento.MaxDate = DateTime.Now;
            dtpIngreso.MaxDate = DateTime.Now;
            lblError.Text = "";
            lblEliminar.Text = "";
        }

        // Definición de una lista de objetos tipo Alumnos llamada listAlumnos
        List<Alumnos> listAlumnos = new List<Alumnos>();

        // Método asociado al botón "agregar"
        private void agregar_Click(object sender, EventArgs e)
        {
            // Variables locales para almacenar los valores ingresados por el usuario
            int legajo;
            int aprobadas;
            bool activo;

            // Comprobamos si los campos de texto tienen valores válidos y no nulos
            if (
                MetodosAuxiliares.ComprobarValor(txtLegajo.Text, out legajo) // Comprobamos que el legajo sea un número válido
                && txtNombre.Text != "" // Comprobamos que el nombre no esté vacío
                && txtApellido.Text != "" // Comprobamos que el apellido no esté vacío
                && dtpNacimiento.Value != DateTime.Now.Date // Comprobamos que la fecha de nacimiento no sea la fecha actual
                && MetodosAuxiliares.ComprobarValor(txtAprobadas.Text, out aprobadas) // Comprobamos que la cantidad de materias aprobadas sea un número válido
                && aprobadas < 37 // Comprobamos que la cantidad de materias aprobadas sea menor a 37
                && MetodosAuxiliares.ComprobarActivo(txtActivo.Text, out activo) // Comprobamos que el estado activo/inactivo sea válido
                )
            {
                // Vaciamos el campo de texto lblError
                lblError.Text = "";

                // Creamos un objeto de tipo Alumnos con los valores ingresados y lo agregamos a la lista listAlumnos
                listAlumnos.Add(
                    new Alumnos
                    (
                        legajo,
                        txtNombre.Text,
                        txtApellido.Text,
                        dtpNacimiento.Value,
                        dtpIngreso.Value,
                        activo,
                        aprobadas
                    )
                );

                // Agregamos los valores del objeto creado a la tabla
                dtgvAlumnos.Rows.Add(
                    listAlumnos.Last().Legajo,
                    listAlumnos.Last().Nombre,
                    listAlumnos.Last().Apellido,
                    listAlumnos.Last().EdadActual(),
                    listAlumnos.Last().Activo
                );

                // Actualizamos los valores de los campos de texto relacionados con el alumno recién creado
                txtEdad.Text = (listAlumnos.Last().EdadDeIngreso()).ToString();
                txtAntiguedad.Text = (listAlumnos.Last().Antiguedad()).ToString();
                txtNoAprobadas.Text = (listAlumnos.Last().MateriasNoAprobadas()).ToString();

                // Vaciamos los campos de texto y definimos valores por defecto para los componentes dtpNacimiento y dtpIngreso
                LimpiarTextBox();
            }
            else
            {
                // Si los campos de texto tienen valores inválidos o nulos, mostramos un mensaje de error en el campo lblError
                lblError.Text = "Error: Campos Vacios o Invalidos";
            }
        }

        // Método asociado al botón "modificar"
        private void modificar_Click(object sender, EventArgs e)
        {
            // Obtenemos el índice de la fila seleccionada
            int filaSeleccionada = dtgvAlumnos.SelectedRows[0].Index;

            // Obtenemos el objeto tipo Alumnos asociado a la fila
            Alumnos alumnoSeleccionado = listAlumnos[filaSeleccionada];

            // Actualizamos los datos del alumno seleccionado con los valores ingresados en los campos de texto
            int legajo;
            int aprobadas;
            bool activo;

            // Comprobamos si los datos ingresados son válidos y los asignamos al alumno seleccionado
            if (MetodosAuxiliares.ComprobarValor(txtLegajo.Text, out legajo) 
                && txtNombre.Text != "" 
                && txtApellido.Text != "" 
                && dtpNacimiento.Value != DateTime.Now.Date 
                && MetodosAuxiliares.ComprobarValor(txtAprobadas.Text, out aprobadas) 
                && aprobadas < 37 
                && MetodosAuxiliares.ComprobarActivo(txtActivo.Text, out activo) 
                )
            {
                // Actualizamos los datos del alumno seleccionado en la lista
                alumnoSeleccionado.Legajo = legajo;
                alumnoSeleccionado.Nombre = txtNombre.Text;
                alumnoSeleccionado.Apellido = txtApellido.Text;
                alumnoSeleccionado.FechaNacimiento = dtpNacimiento.Value;
                alumnoSeleccionado.FechaIngreso = dtpIngreso.Value;
                alumnoSeleccionado.Activo = activo;
                alumnoSeleccionado.MateriasAprobadas = aprobadas;

                // Actualizamos los datos del alumno seleccionado en la tabla
                dtgvAlumnos.Rows[filaSeleccionada].Cells[0].Value = legajo;
                dtgvAlumnos.Rows[filaSeleccionada].Cells[1].Value = txtNombre.Text;
                dtgvAlumnos.Rows[filaSeleccionada].Cells[2].Value = txtApellido.Text;
                dtgvAlumnos.Rows[filaSeleccionada].Cells[3].Value = alumnoSeleccionado.EdadActual();
                dtgvAlumnos.Rows[filaSeleccionada].Cells[4].Value = activo;

                // Actualizamos los campos de texto que muestran información adicional del alumno seleccionado
                txtEdad.Text = alumnoSeleccionado.EdadDeIngreso().ToString();
                txtAntiguedad.Text = alumnoSeleccionado.Antiguedad().ToString();
                txtNoAprobadas.Text = alumnoSeleccionado.MateriasNoAprobadas().ToString();

                // Limpiamos los campos de texto
                LimpiarTextBox();
            }
            else
            {
                // Mostramos un mensaje de error si alguno de los campos es inválido o está vacío
                lblError.Text = "Error: Campos Vacios o Invalidos";
            }

        }

        // Método que borra un alumno de la lista y de la tabla de alumnos
        private void borrar_Click(object sender, EventArgs e)
        {
            // Comprobamos si se ha seleccionado una sola fila
            if (dtgvAlumnos.SelectedRows.Count == 1)
            {
                lblEliminar.Text = "";

                // Obtenemos el indice de la fila seleccionada
                int filaSeleccionada = dtgvAlumnos.SelectedRows[0].Index;

                // Asignamos null al objeto Alumno en la lista de Alumnos
                listAlumnos[filaSeleccionada] = null;

                // Eliminamos el objeto Alumno de la lista de Alumnos
                listAlumnos.RemoveAt(filaSeleccionada);

                // Eliminamos la fila correspondiente de la tabla DataGridView
                dtgvAlumnos.Rows.RemoveAt(filaSeleccionada);

                // Llamamos al método LimpiarTextBox para borrar los datos de los TextBox
                LimpiarTextBox();
            }
            else
            {
                // Si no se ha seleccionado una fila, mostramos un mensaje de error en la etiqueta "lblEliminar"
                lblEliminar.Text = "No ha seleccionado ningun campo";
            }
        }

        // Método que se ejecuta al seleccionar una fila en la tabla DataGridView
        private void dtgvAlumnos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {        
            int index = e.RowIndex;

            // Verifica si se seleccionó una fila (y no el encabezado)
            if (e.RowIndex >= 0) 
            {
                AsignarCeldas(index);
            }
        }

        private void dtgvAlumnos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            // Verifica si se seleccionó una fila (y no el encabezado)
            if (e.RowIndex >= 0) 
            {
                AsignarCeldas(index);
            }

        }

        // Método que se ejecuta al salir del mouse de una celda de la tabla DataGridView
        private void dtgvAlumnos_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            // Si no hay filas seleccionadas, llamamos al método LimpiarTextBox para borrar los datos de los TextBox
            if (dtgvAlumnos.SelectedRows.Count == 0)
            {
                LimpiarTextBox();
            }
        }

        // Método que borra los datos de los TextBox
        private void LimpiarTextBox()
        {
            txtLegajo.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";
            txtAntiguedad.Text = "";
            txtAprobadas.Text = "";
            txtNoAprobadas.Text = "";
            txtActivo.Text = "";
            dtpNacimiento.Value = new DateTime(2000, 1, 1);
            dtpIngreso.Value = new DateTime(2023, 1, 1);
        }

        // Método que asigna los datos de la fila seleccionada a los TextBox correspondientes
        private void AsignarCeldas(int index)
        {
            txtLegajo.Text = listAlumnos[index].Legajo.ToString();
            txtNombre.Text = listAlumnos[index].Nombre;
            txtApellido.Text = listAlumnos[index].Apellido;
            txtEdad.Text = (listAlumnos.Last().EdadDeIngreso()).ToString();
            txtAntiguedad.Text = (listAlumnos.Last().Antiguedad()).ToString();
            txtAprobadas.Text = listAlumnos[index].MateriasAprobadas.ToString();
            txtNoAprobadas.Text = (listAlumnos.Last().MateriasNoAprobadas()).ToString();
            txtActivo.Text = listAlumnos[index].Activo.ToString();
            dtpNacimiento.Value = listAlumnos[index].FechaNacimiento;
            dtpIngreso.Value = listAlumnos[index].FechaIngreso;
        }
    }
}