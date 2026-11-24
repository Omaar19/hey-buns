// Saca un mensaje entendible de un error de Axios
export function obtenerMensajeError(e: any): string {
  const data = e?.response?.data;
  if (data?.mensaje) return data.mensaje;
  if (data?.errors) {
    const primero = Object.values(data.errors)[0] as string[];
    if (primero?.length) return primero[0];
  }
  if (e?.response) return "Ocurrió un error en el servidor";
  return "No se pudo conectar con el servidor, revisa que la API esté corriendo y la URL en el archivo .env";
}
