using System;
using System.Net;
using System.Text;

namespace ByteBank.Portal.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;
        public WebApplication(string[] prefixos)
        {
            if (prefixos == null)
            {
                throw new ArgumentException(nameof(prefixos));
            }
            _prefixos = prefixos;
        }
        public void Iniciar()
        {
            var httpListener = new HttpListener();

            foreach (var prefixo in _prefixos)
            {
                httpListener.Prefixes.Add(prefixo);
            }

            httpListener.Start();

            var contexto = httpListener.GetContext();
            var requisicao = contexto.Request;
            var resposta = contexto.Response;

            var respostaConteudo = "Hello Word";
            var respostaConteudoBites = Encoding.UTF8.GetBytes(respostaConteudo);

            resposta.ContentType = "text/html; charset=UTF-8";
            resposta.StatusCode = 200;
            resposta.ContentLength64 = respostaConteudoBites.Length;

            resposta.OutputStream.Write(respostaConteudoBites, 0, respostaConteudoBites.Length);

            resposta.OutputStream.Close();

            httpListener.Stop();

        }
    }
}
