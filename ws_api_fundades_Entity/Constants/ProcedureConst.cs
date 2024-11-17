namespace ws_api_fundades_Entity.Constants
{
    public class ProcedureConst
    {
        //SCHEMA
        private static string SCHEMA = "ApiRest.";

        //PROCEDURES

        //Users
        public static string SP_CREAR_USUARIO = $"{SCHEMA}crear_usuario";
        public static string SP_LOGIN_USUARIO = $"{SCHEMA}login_usuario";
        
        //Cuestionarios
        public static string SP_LIST_CUESTIONARIO = $"{SCHEMA}obtener_cuestionario";
        public static string SP_LIST_PUESTOTRABAJO = $"{SCHEMA}puestos_trabajo_activos";
        public static string SP_CREAR_POSTULACION = $"{SCHEMA}registrar_postulacion";
        public static string SP_EVALUAR_POSTULACION = $"{SCHEMA}evaluar_postulacion";

        //Puesto
        public static string SP_CREAR_PUESTO = $"{SCHEMA}crear_puesto_trabajo";
        public static string SP_DESACTIVAR_PUESTO = $"{SCHEMA}desactivar_puesto_trabajo";
        public static string SP_LIST_PUESTO_INACTIVO = $"{SCHEMA}puestos_trabajo_inactivos";
        public static string SP_ACTIVAR_PUESTO = $"{SCHEMA}activar_puesto_trabajo";
        public static string SP_LIST_POSTULANTES_PUESTO = $"{SCHEMA}puestos_trabajo_postulantes";

        //Logs
        public static string SP_LOG_API = $"{SCHEMA}save_api_log";

    }
}
