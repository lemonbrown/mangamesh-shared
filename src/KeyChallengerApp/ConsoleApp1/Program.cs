// See https://aka.ms/new-console-template for more information
using NSec.Cryptography;
using System.Text.Json;

//// Create an exportable Ed25519 key
//var creationParameters = new KeyCreationParameters
//{
//    ExportPolicy = KeyExportPolicies.AllowPlaintextExport
//};

//using var key = new Key(SignatureAlgorithm.Ed25519, creationParameters);

//// Now you can export private and public key
//byte[] privateKeyBytes = key.Export(KeyBlobFormat.RawPrivateKey);
//byte[] publicKeyBytes = key.Export(KeyBlobFormat.RawPublicKey);

//Console.WriteLine("Private key Base64: " + Convert.ToBase64String(privateKeyBytes));
//Console.WriteLine("Public key Base64: " + Convert.ToBase64String(publicKeyBytes));



// --------------------------
// 1️⃣ Inputs
// Replace these with your actual values
// --------------------------
string privateKeyBase64 = "qCJgbX8pZBBh9sN7vsiaN9WCxr9bJ9L8b3SwooSSW1k=";
string challengeId = "eec691465169489893e94d5c0f1011c7";
string nonceBase64 = "GdXogDAtCaQBKio2npfaXRyzqKUTG06YMZto7+EJ7GM=";

// --------------------------
// 2️⃣ Decode inputs
// --------------------------
byte[] privateKeyBytes = Convert.FromBase64String(privateKeyBase64);
byte[] nonceBytes = Convert.FromBase64String(nonceBase64);

// --------------------------
// 3️⃣ Import private key (raw)
// --------------------------
var creationParams = new KeyCreationParameters
{
    ExportPolicy = KeyExportPolicies.AllowPlaintextExport
};

using var key = Key.Import(SignatureAlgorithm.Ed25519, privateKeyBytes, KeyBlobFormat.RawPrivateKey);

// --------------------------
// 4️⃣ Sign the nonce
// --------------------------
byte[] signatureBytes = SignatureAlgorithm.Ed25519.Sign(key, nonceBytes);

if (signatureBytes.Length != 64)
{
    Console.WriteLine($"ERROR: Signature length is {signatureBytes.Length}, expected 64 bytes!");
    return;
}

string signatureBase64 = Convert.ToBase64String(signatureBytes);

Console.WriteLine(signatureBase64);

