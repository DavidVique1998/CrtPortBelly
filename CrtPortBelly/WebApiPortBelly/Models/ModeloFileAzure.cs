using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApiPortBelly.Models
{
    public class ModeloFileAzure :IDisposable
    {
        CloudFileDirectory directorio;
        CloudFile fichero;
        public ModeloFileAzure()
        {
            // Para obtener acceso a la cuenta storage debemos usar sus claves
            // Para ello recuperamos la cadena de conexion  que hemos incluido en el web.config (usamos el valor key)
            string claves = CloudConfigurationManager.GetSetting("storagefile");
            // Con las claves accedemos a la cuenta storage
            CloudStorageAccount cuenta = CloudStorageAccount.Parse(claves);
            // Para acceder a nuestro recurso debemos crear un cliente del tipo de recurso que que queramos (en este casofile)
            CloudFileClient cliente = cuenta.CreateCloudFileClient();
            //Con  nuestro cliente de tipo File recuperamos el recurso compartido que hemos creado antes llamado ficheros
            CloudFileShare recurso = cliente.GetShareReference("ficheros");
            // Para guardar Archivos en la raiz de nuestro recurso accdemos al directorio
            this.directorio = recurso.GetRootDirectoryReference();
        }

        public void SubirFicheroAzure(string nombre, Stream contenido)
        {
            try
            {
                // Accedemos a la referencia del archivo por su nombre;
                fichero = this.directorio.GetFileReference(nombre);
                // Escribimos el contenido
                fichero.UploadFromStream(contenido);
            }
            catch (Exception)
            {
            }
            
        }

        public List<string> GetArchivosAzure()
        {
            try
            {
                List<string> archivos = new List<string>();
                // Recuperamos los archivos del directorio
                IEnumerable<IListFileItem> datos = this.directorio.ListFilesAndDirectories();
                //Recorremos los datos 
                foreach (IListFileItem item in datos)
                {
                    // Cogemos la ruta Uri de cada recurso
                    string rutaUri = item.Uri.ToString();
                    // Guardamos el nombre del archivo que obtenemso de la ruta
                    int pos = rutaUri.IndexOf("/") + 1;
                    string nombreArchivo = rutaUri.Substring(pos);
                    archivos.Add(nombreArchivo);
                }
                return archivos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public byte[] GetFile(string nombreArchivo)
        {
            try
            {
                // Buscamos el nombre de nuestro archivo en nuestro directorio
                CloudFile cloudFile = this.directorio.GetFileReference(nombreArchivo);
                // Descargamos el contenido del archivo como file
                using (var content = new MemoryStream())
                {
                    cloudFile.DownloadToStream(content);
                    return content.ToArray();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void DeleteFile(string fileName)
        {
            try
            {
                CloudFile file = this.directorio.GetFileReference(fileName);
                file.DeleteIfExists();
            }
            catch (Exception)
            {
            }
            
        }

        public bool ComprobarArchivo(string Name)
        {
            try
            {
                return this.directorio.GetFileReference(Name).Exists();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

        }
    }
}