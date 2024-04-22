using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CertificateUpdater : MonoBehaviour
{
    void Start()
    {
        UpdateCACertificates();
    }

    private void UpdateCACertificates()
    {
        byte[] pemData = LoadPEMFile("cacert-2024-03-11.pem");
        if (pemData == null)
        {
            Debug.LogError("Failed to load cacert.pem file.");
            return;
        }

        X509Certificate2Collection certificates = new X509Certificate2Collection();
        X509Certificate2 certificate = new X509Certificate2(pemData);
        certificates.Add(certificate);

        System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
        {
            foreach (X509Certificate2 ca in certificates)
            {
                chain.ChainPolicy.ExtraStore.Add(ca);
            }
            chain.Build((X509Certificate2)cert);
            return true;
        };
    }

    private byte[] LoadPEMFile(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            return File.ReadAllBytes(filePath);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }
}