import { useState } from "react";
import { ActivityIndicator, Alert, StyleSheet, Text, TextInput, TouchableOpacity, View } from "react-native";
import { Link } from "expo-router";
import { useAuth } from "../../src/context/AuthContext";
import { obtenerMensajeError } from "../../src/utils/errores";

export default function Login() {
  const { login } = useAuth();
  const [correo, setCorreo] = useState("");
  const [password, setPassword] = useState("");
  const [enviando, setEnviando] = useState(false);

  const iniciarSesion = async () => {
    if (!correo.includes("@") || password.length === 0) {
      Alert.alert("Datos incompletos", "Escribe tu correo y tu contraseña");
      return;
    }
    try {
      setEnviando(true);
      await login(correo.trim(), password);
    } catch (e) {
      Alert.alert("No se pudo iniciar sesión", obtenerMensajeError(e));
    } finally {
      setEnviando(false);
    }
  };

  return (
    <View style={estilos.contenedor}>
      <Text style={estilos.titulo}>Hey Buns</Text>

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
        placeholder="Contraseña"
        secureTextEntry
        value={password}
        onChangeText={setPassword}
      />

      <TouchableOpacity style={estilos.boton} onPress={iniciarSesion} disabled={enviando}>
        {enviando ? <ActivityIndicator color="#fff" /> : <Text style={estilos.textoBoton}>Iniciar sesión</Text>}
      </TouchableOpacity>

      <Link href="/registro" style={estilos.link}>
        ¿No tienes cuenta? Regístrate
      </Link>
    </View>
  );
}

const estilos = StyleSheet.create({
  contenedor: { flex: 1, justifyContent: "center", padding: 24, backgroundColor: "#fff" },
  titulo: { fontSize: 32, fontWeight: "bold", textAlign: "center", marginBottom: 32 },
  input: { borderWidth: 1, borderColor: "#999", borderRadius: 6, padding: 12, marginBottom: 16, fontSize: 16 },
  boton: { backgroundColor: "#d35400", padding: 14, borderRadius: 6, alignItems: "center", marginTop: 8 },
  textoBoton: { color: "#fff", fontSize: 16, fontWeight: "bold" },
  link: { textAlign: "center", marginTop: 24, color: "#d35400", fontSize: 15 },
});
