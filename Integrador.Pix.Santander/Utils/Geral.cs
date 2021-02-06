using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Integrador.Pix.Santander.Utils
{
    public static class Geral
    {
        public static string ApenasNumeros(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return "";

            var onlyNumber = "";
            foreach (var s in valor)
            {
                if (char.IsNumber(s))
                    onlyNumber += s;
            }
            return onlyNumber.Trim();
        }

        public static int CalcularIdade(DateTime? dataNascimento)
        {
            if (dataNascimento.HasValue)
            {
                var hoje = DateTime.Today;
                var idade = hoje.Year - dataNascimento.Value.Year;
                if (dataNascimento.Value.Date > hoje.AddYears(-idade)) idade--;

                return idade;
            }
            return 0;
        }

        public static decimal ValorTruncado(decimal valor, int precisao)
        {
            decimal fator = (decimal)Math.Pow(10d, precisao);
            decimal valorTruncado = Math.Floor(valor * fator);
            return Math.Floor((Math.Round(valorTruncado, precisao))) / fator;
        }

        public static string MesPorExtenso(int mes, bool retornarSigla = false)
        {
            string retorno = "Indefinido";
            switch (mes)
            {
                case 1: retorno = "Janeiro"; break;
                case 2: retorno = "Fevereiro"; break;
                case 3: retorno = "Março"; break;
                case 4: retorno = "Abril"; break;
                case 5: retorno = "Maio"; break;
                case 6: retorno = "Junho"; break;
                case 7: retorno = "Julho"; break;
                case 8: retorno = "Agosto"; break;
                case 9: retorno = "Setembro"; break;
                case 10: retorno = "Outubro"; break;
                case 11: retorno = "Novembro"; break;
                case 12: retorno = "Dezembro"; break;
            }

            if (retornarSigla)
                retorno = retorno.ToUpper().Substring(0, 3);

            return retorno;
        }

        public static int ObterDiaSemana(DayOfWeek dayOfWeek)
        {
            var retorno = 0;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: retorno = 2; break;
                case DayOfWeek.Tuesday: retorno = 3; break;
                case DayOfWeek.Wednesday: retorno = 4; break;
                case DayOfWeek.Thursday: retorno = 5; break;
                case DayOfWeek.Friday: retorno = 6; break;
                case DayOfWeek.Saturday: retorno = 7; break;
                case DayOfWeek.Sunday: retorno = 1; break;
            }

            return retorno;
        }

        public static string FormatarMoeda(decimal? valor)
        {
            return string.Format("{0:C}", valor ?? 0);
        }

        public static string RemoverCaracteresEspeciais(string texto)
        {
            texto = RemoverAcentos(texto);

            string numeros = "0;1;2;3;4;5;6;7;8;9";
            string letras = " ;a;A;b;B;c;C;d;D;e;E;f;F;g;G;h;H;i;I;j;J;k;K;l;L;m;M;n;N;o;O;p;P;q;Q;r;R;s;S;t;T;u;U;v;V;x;X;w;W;y;Y;z;Z";

            texto = ManterCaracteresValidos(texto, numeros + ";" + letras);

            return texto;
        }

        public static string RemoverAcentos(string texto)
        {
            //a
            texto = texto.Replace("á", "a");
            texto = texto.Replace("à", "a");
            texto = texto.Replace("â", "a");
            texto = texto.Replace("ã", "a");
            texto = texto.Replace("ä", "a");
            texto = texto.Replace("ª", "a");
            //A
            texto = texto.Replace("Á", "A");
            texto = texto.Replace("À", "A");
            texto = texto.Replace("Â", "A");
            texto = texto.Replace("Ã", "A");
            texto = texto.Replace("Ä", "A");
            //e
            texto = texto.Replace("é", "e");
            texto = texto.Replace("è", "e");
            texto = texto.Replace("ê", "e");
            texto = texto.Replace("ë", "e");
            //E
            texto = texto.Replace("É", "E");
            texto = texto.Replace("È", "E");
            texto = texto.Replace("Ê", "E");
            texto = texto.Replace("Ë", "E");
            //i
            texto = texto.Replace("í", "i");
            texto = texto.Replace("ì", "i");
            texto = texto.Replace("î", "i");
            texto = texto.Replace("ï", "i");
            //I
            texto = texto.Replace("Í", "I");
            texto = texto.Replace("Ì", "I");
            texto = texto.Replace("Î", "I");
            texto = texto.Replace("Ï", "I");
            //o
            texto = texto.Replace("ó", "o");
            texto = texto.Replace("ò", "o");
            texto = texto.Replace("ô", "o");
            texto = texto.Replace("õ", "o");
            texto = texto.Replace("º", "o");
            texto = texto.Replace("°", "o");
            //O
            texto = texto.Replace("Ó", "O");
            texto = texto.Replace("Ò", "O");
            texto = texto.Replace("Ô", "O");
            texto = texto.Replace("Õ", "O");
            //u
            texto = texto.Replace("ú", "u");
            texto = texto.Replace("ù", "u");
            texto = texto.Replace("û", "u");
            texto = texto.Replace("ü", "u");
            //U
            texto = texto.Replace("Ú", "U");
            texto = texto.Replace("Ù", "U");
            texto = texto.Replace("Û", "U");
            texto = texto.Replace("Ü", "U");

            // Ç/ç
            texto = texto.Replace("ç", "c");
            texto = texto.Replace("Ç", "C");

            return texto;
        }

        public static string AplicarMascaraTelefone(string telefone)
        {
            var retorno = "";
            telefone = Geral.ApenasNumeros(telefone);
            if (!string.IsNullOrEmpty(telefone))
            {
                if (telefone.Length == 11)
                    retorno = String.Format("{0:(##) #####-####}", double.Parse(telefone));
                if (telefone.Length == 10)
                    retorno = String.Format("{0:(##) ####-####}", double.Parse(telefone));
            }

            return retorno;
        }

        public static string ManterCaracteresValidos(string campo, string listaCaracteresPossiveis)
        {
            int i = 0;
            bool achou;

            while (i <= (campo.Length - 1))
            {
                achou = false;

                foreach (string item in listaCaracteresPossiveis.Split(';'))
                {
                    if (Convert.ToString(campo[i]) == item)
                    {
                        achou = true;
                        break;
                    }
                }

                if (!achou)
                {
                    campo = campo.Replace(Convert.ToString(campo[i]), "");
                }
                else
                {
                    i++;
                }
            }

            return campo;
        }

        public static string ListaPorExtenso(IEnumerable<string> lista)
        {
            if (lista == null) return "";
            if (lista.Count() == 1) return lista.First();

            var ultimoItem = lista.Last();
            var primeirosItens = lista.Take(lista.Count() - 1);

            var retorno = string.Join(", ", primeirosItens);
            retorno = $"{retorno} e {ultimoItem}";
            return retorno;
        }

        public static string ObterCampoFormatado(this string valor, int tamanho = 0, bool limparCampos = false)
        {
            if (limparCampos)
                valor = RemoverCaracteresEspeciais(valor);

            if (valor.Length > tamanho && tamanho > 0)
                valor = valor.Substring(0, tamanho);

            var len = valor.Length.ToString().PadLeft(2, '0');
            valor = $"{len}{valor}";

            return valor;
        }

        public static byte[] HexToBytes(string input)
        {
            byte[] result = new byte[input.Length / 2];
            for (int i = 0; i < result.Length; i++)
                result[i] = Convert.ToByte(input.Substring(2 * i, 2), 16);
            return result;
        }
    }
}
