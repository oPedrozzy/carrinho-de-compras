using System;
using System.Collections.Generic;
using System.Linq;

namespace CarrinhoCompras
{
    public class Program
    {
        private static List<Produto> _Produtos = new List<Produto>();
        private static List<Produto> _Carrinho = new List<Produto>();
        private static string _ReadLine = String.Empty;

        public static void Main(string[] args)
        {
            CarregarProduto();

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Olá Seja bem vindo ao Carrinho de Compras");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Eu sou seu assistente de vendas on-line.");
            Console.WriteLine("Vamos começar ?");


            Pesquisar();

            _ReadLine = Console.ReadLine();
        }


        private class Produto
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public decimal Valor { get; set; }
        }

        private static void CarregarProduto()
        {
            _Produtos.Add(new Produto() { Id = 1, Nome = "Copo Stanley 100ml", Valor = 100 });
            _Produtos.Add(new Produto() { Id = 2, Nome = "Copo Stanley 200ml", Valor = 200 });
            _Produtos.Add(new Produto() { Id = 3, Nome = "Copo Stanley 300ml", Valor = 300 });
        }

        private static void Pesquisar()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Muito bem, digite o produto que você deseja adquirir, pode ser o seu nome, ou uma descrição.");
            Console.WriteLine("Ou digite 'F' para Finalizar sua compra.");
            Console.WriteLine("------------------------------------------");
            _ReadLine = Console.ReadLine();
            if (_ReadLine.Trim().ToUpper() == "F")
            {
                Finalizar();
                return;
            }

            List<Produto> produtosPesquisa = _Produtos.Where(x => x.Nome.ToUpper().Contains(_ReadLine.ToUpper())).ToList();

            // Pesquisa não encontrada
            if (produtosPesquisa.Any() == false)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Huuumm, não encontramos esse produto no nosso catálogo, sinto muito.");
                Console.WriteLine("------------------------------------------");
                Pesquisar();
                return;
            }

            // Pesquisa Encontrada
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Temos boas notícias, você tem produtos disponiveis, dê uma olhada: ");
            Console.WriteLine("------------------------------------------");
            PesquisarListar(produtosPesquisa, false);
            Console.WriteLine("Digite os codigos dos produtos que você deseja comprar, separados por virgula: (Ex: 1,2,5).");
            Console.WriteLine("Ou digite 'F' para finalizar sua compra.");
            Console.WriteLine("------------------------------------------");

            // Finalizar
            _ReadLine = Console.ReadLine();
            if (_ReadLine.Trim().ToUpper() == "F")
            {
                Finalizar();
                return;
            }

            // Produtos Selecionados
            Validar(_ReadLine);

            if (_Carrinho.Any())
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Produtos adicionados ao carrinho !!!");
                PesquisarListar(_Carrinho, true);
                Console.WriteLine("------------------------------------------");
                Pesquisar();
            }
            else
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Produtos Inválidos.");
                Console.WriteLine("------------------------------------------");
                Pesquisar();
            }
        }

        private static void PesquisarListar(List<Produto> produtos, bool finalizar)
        {
            Console.WriteLine("------------------------------------------");
            foreach (Produto produto in produtos)
                Console.WriteLine($"CODIGO: {produto.Id} | {produto.Nome} | {produto.Valor.ToString("N2")}");
            Console.WriteLine("------------------------------------------");

            if(finalizar)
            {
                Console.WriteLine($"VALOR TOTAL DA SUA COMPRA: {produtos.Sum(x => x.Valor).ToString("N2")}");
                Console.WriteLine("------------------------------------------");
            }
        }

        private static void Validar(string produtos)
        {
            string[] produtosSelecionados = produtos.Split(',');

            if (produtosSelecionados.Count() == 0)
            {
                Console.WriteLine("Produtos Inválidos.");
                Pesquisar();
                return;
            }

            int id;
            foreach (string produto in produtosSelecionados)
            {
                if (int.TryParse(produto.Trim(), out id) == false)
                {
                    Console.WriteLine("Produtos Inválidos.");
                    Pesquisar();
                    return;
                }

                Produto produtoCarrinho = _Produtos.Where(x => x.Id == id).FirstOrDefault();
                if(produtoCarrinho != null)
                    _Carrinho.Add(produtoCarrinho);
            }
        }

        private static void Finalizar()
        {
            // Carrinho Vazio
            if (_Carrinho.Any() == false)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Você finalizou sua compra SEM NENHUM PRODUTO, mas agradecemos pela preferencia.");
                Console.WriteLine("------------------------------------------");
                return;
            }

            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Muito bem, já vamos finalizar sua compra, fique tranquilo.");
            PesquisarListar(_Carrinho, true);
            Console.WriteLine("Digite 'C' para Confirmar sua compra, ou 'V' para Voltar a sua compra.");
            Console.WriteLine("------------------------------------------");
            _ReadLine = Console.ReadLine();

            if (_ReadLine.Trim().ToUpper() == "C")
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("PARABENS, Você finalizou sua compra com sucesso, agradecemos pela preferencia !!!!");
                PesquisarListar(_Carrinho, true);
                Console.WriteLine("------------------------------------------");
                return;
            }

            Pesquisar();
        }

    }

}
