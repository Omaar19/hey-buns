import { StyleSheet, Text, TouchableOpacity, View } from "react-native";
import { useAuth } from "../../src/context/AuthContext";

export default function Inicio() {
  const { usuario, logout } = useAuth();

  return (
    <View style={estilos.contenedor}>
      <Text style={estilos.titulo}>Hey Buns</Text>
      <Text style={estilos.saludo}>Hola, {usuario?.nombre}</Text>
      <Text style={estilos.rol}>Rol: {usuario?.rol}</Text>

      <TouchableOpacity style={estilos.boton} onPress={logout}>
        <Text style={estilos.textoBoton}>Cerrar sesión</Text>
      </TouchableOpacity>
    </View>
  );
}

const estilos = StyleSheet.create({
  contenedor: { flex: 1, justifyContent: "center", padding: 24, backgroundColor: "#fff" },
  titulo: { fontSize: 32, fontWeight: "bold", textAlign: "center", marginBottom: 16 },
  saludo: { fontSize: 20, textAlign: "center" },
  rol: { fontSize: 16, textAlign: "center", marginTop: 8, marginBottom: 32 },
  boton: { backgroundColor: "#d35400", padding: 14, borderRadius: 6, alignItems: "center" },
  textoBoton: { color: "#fff", fontSize: 16, fontWeight: "bold" },
});
