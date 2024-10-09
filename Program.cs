using iTextSharp.text;
using iTextSharp.text.pdf;

void PlacerCode2D(string strChaineCode2D, PdfContentByte cb, int positionX, int positionY)
{
    // Création de l'objet DataMatrix en utilisant QRCodeWriter de iTextSharp
    BarcodeDatamatrix dm = new()
    {
        Options = BarcodeDatamatrix.DM_ASCII
    };

    dm.Generate(strChaineCode2D); // Génère le code DataMatrix avec la chaîne donnée

    // Récupérer l'image du DataMatrix
    Image image = dm.CreateImage();
    // Positionner l'image à la position souhaitée
    image.SetAbsolutePosition(positionX, positionY);

    // Ajouter l'image au contenu de la page
    cb.AddImage(image);
}


var pdf = @"Unit\Helpers\Artefacts\helloworld.pdf";
using FileStream fichier = new(pdf, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
using MemoryStream fichierPdfAvecEstampille = new MemoryStream();


PdfReader reader = new(fichier, false);
PdfReader.AllowOpenWithFullPermissions = true;

using (PdfStamper stamper = new(reader, fichierPdfAvecEstampille))
{
    PlacerCode2D("Allo", stamper.GetOverContent(1), 75, 705);

    stamper.Close();
}

File.WriteAllBytes("output.pdf", fichierPdfAvecEstampille.ToArray());

Console.WriteLine("Hello, World!");


