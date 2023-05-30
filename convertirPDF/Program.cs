using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ImageToPdfConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entrez le chemin complet du dossier contenant les images :");
            string folderPath = Console.ReadLine();

            Console.WriteLine("Entrez le nom du fichier PDF de sortie (sans l'extension) :");
            string pdfFileName = Console.ReadLine();

            Console.WriteLine("Conversion en cours...");

            List<string> imageExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
            List<string> imageFiles = new List<string>();
            foreach (string file in Directory.GetFiles(folderPath))
            {
                string extension = Path.GetExtension(file).ToLower();
                if (imageExtensions.Contains(extension))
                {
                    imageFiles.Add(file);
                }
            }

            if (imageFiles.Count > 0)
            {
                using (Document document = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(folderPath, pdfFileName + ".pdf"), FileMode.Create));
                    document.Open();

                    foreach (string imageFile in imageFiles)
                    {
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageFile);

                        // Ajuster la taille de la page en fonction de l'image
                        document.SetPageSize(new Rectangle(image.Width, image.Height));
                        document.NewPage();

                        // Ajouter l'image à la page PDF
                        document.Add(image);
                    }

                    document.Close();
                }

                Console.WriteLine($"PDF de {imageFiles.Count} pages généré.");
            }
            else
            {
                Console.WriteLine("Aucune image trouvée.");
            }

            Console.ReadLine();
        }
    }
}
