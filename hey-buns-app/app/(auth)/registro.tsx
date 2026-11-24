import { useState } from "react";
import { ActivityIndicator, Alert, StyleSheet, Text, TextInput, TouchableOpacity, View } from "react-native";
import { Link } from "expo-router";
import { useAuth } from "../../src/context/AuthContext";
import { obtenerMensajeError } from "../../src/utils/errores";

export default function Registro() {
  const { registro } = useAuth();
  const [nombre, setNombre] = useState("");
  const [correo, setCorreo] = useState("");
  const [password, setPassword] = useState("");
  const [enviando, setEnviando] = useState(false);

  const crearCuenta = async () => {
    if (nombre.trim().length === 0) {
      Alert.alert("Datos incompletos", "Escribe tu nombre");
      return;
    }
    if (!correo.includes("@")) {
      Alert.alert("Datos incompletos", "Escribe un correo válido");
      return;
    }
    if (password.length < 6) {
      Alert.alert("Datos incompletos", "La contraseña debe tener al menos 6 caracteres");
      return;
    }
    try {
      setEnviando(true);
      await registro(nombre.trim(), correo.trim(), password);
    } catch (e) {
      Alert.alert("No se pudo crear la cuenta", obtenerMensajeError(e));
    } finally {
      setEnviando(false);
    }
  };

  return (
    <View style={estilos.contenedor}>
      <Text style={estilos.titulo}>Crear cuenta</Text>

      <TextInput style={estilos.input} placeholder="Nombre" value={nombre} onChangeText={setNombre} />
      <TextInput
        style={estilos.input}
        placeholder="Correo"
        autoCapitalize="none"
        keyboardType="email-address"
        value={correo}
        onChangeText={setCorreo}
      />
      <TextInput
        style={estilos.input}
        placeholder="Contraseña (mínimo 6 caracteres)"
        secureTextEntry
        value={password}
        onChangeText={setPassword}
      />

      <TouchableOpacity style={estilos.boton} onPress={crearCuenta} disabled={enviando}>
        {enviando ? <ActivityIndicator color="#fff" /> : <Text style={estilos.textoBoton}>Registrarme</Text>}
      </TouchableOpacity>

      <Link href="/login" style={estilos.link}>
        ¿Ya tienes cuenta? Inicia sesión
      </Link>
    </View>
  );
}

const estilos = StyleSheet.create({
  contenedor: { flex: 1, justifyContent: "center", padding: 24, backgroundColor: "#fff" },
  titulo: { fontSize: 28, fontWeight: "bold", textAlign: "center", marginBottom: 32 },
  input: { borderWidth: 1, borderColor: "#999", borderRadius: 6, padding: 12, marginBottom: 16, fontSize: 16 },
  boton: { backgroundColor: "#d35400", padding: 14, borderRadius: 6, alignItems: "center", marginTop: 8 },
  textoBoton: { color: "#fff", fontSize: 16, fontWeight: "bold" },
  link: { textAlign: "center", marginTop: 24, color: "#d35400", fontSize: 15 },
});
