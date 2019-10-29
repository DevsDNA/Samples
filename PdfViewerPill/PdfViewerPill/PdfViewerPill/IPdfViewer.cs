using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PdfViewerPill
{
    public interface IPdfViewer
    {
        Task Open(string filePath);
        
    }
}
