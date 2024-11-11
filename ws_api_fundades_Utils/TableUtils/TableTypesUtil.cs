using System;
using System.Collections.Generic;
using System.Data;
using ws_api_fundades_Entity.Models.Request;

public class TableConverter
{
    public static DataTable ConvertToDataTable(MRCrearPostulacion postulacion)
    {
        DataTable table = new DataTable();
        table.Columns.Add(new DataColumn("usuario", typeof(string)));
        table.Columns.Add(new DataColumn("fechaPostulacion", typeof(DateTime)));
        table.Columns.Add(new DataColumn("puesto", typeof(string)));
        table.Columns.Add(new DataColumn("pregunta", typeof(string)));
        table.Columns.Add(new DataColumn("tipoPregunta", typeof(string)));
        table.Columns.Add(new DataColumn("respuesta", typeof(int)));

        foreach (var respuesta in postulacion.respuestas)
        {
            DataRow row = table.NewRow();
            row["usuario"] = postulacion.usuario;
            row["fechaPostulacion"] = postulacion.fechaPostulacion;
            row["puesto"] = postulacion.puesto;
            row["pregunta"] = respuesta.pregunta;
            row["tipoPregunta"] = respuesta.tipoPregunta;
            row["respuesta"] = respuesta.respuesta;

            table.Rows.Add(row);
        }

        return table;
    }
}
