using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DAL.Model;


// Regras de Negócio da aplicação
// Efetua operações SELECT, UPDATE, INSERT, DELETE, BUSCA POR ID


namespace DAL.Persistence
{
    public class PessoaDAL: Conexao
    {
        public Pessoa p { get; set; }

        // Método para gravar um novo registro na tabela Pessoa
        public void Gravar(Pessoa p)
        {
            try
            {
                AbrirConexao();
                
                // Instancia o comando SQL de inserção
                Cmd = new SqlCommand("INSERT INTO PESSOA (NOME, ENDERECO, EMAIL) VALUES(@V1, @V2, @V3)", Con);
                
                // Alimenta os parâmetros necessários
                Cmd.Parameters.AddWithValue("@V1", p.Nome);
                Cmd.Parameters.AddWithValue("@V2", p.Endereco);
                Cmd.Parameters.AddWithValue("@v3", p.Email);

                //Executa o Insert
                Cmd.ExecuteNonQuery();          

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar Pessoa: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        // Método para atualizar informações
        public void Atualizar(Pessoa p)
        {
            try
            {
                AbrirConexao();

                Cmd = new SqlCommand("UPDATE PESSOA SET NOME= @V1, ENDERECO=@V2, EMAIL=@V3 WHERE CODIGO = @V4", Con);

                Cmd.Parameters.AddWithValue("@V1", p.Nome);
                Cmd.Parameters.AddWithValue("@V2", p.Endereco);
                Cmd.Parameters.AddWithValue("@V3", p.Email);
                Cmd.Parameters.AddWithValue("@V4", p.Codigo);

                Cmd.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao atualizar Pessoa: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        // Método para exluir pessoa por código
        public void Excluir(int Codigo)
        {
            try
            {
                AbrirConexao();

                Cmd = new SqlCommand("DELETE FROM PESSOA WHERE CODIGO WHERE CODIGO = @V1", Con);

                Cmd.Parameters.AddWithValue("@V1", Codigo);
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {                
                throw new Exception("Erro ao excluir Pessoa! " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        // Método para pesquisar por código
        public Pessoa PesquisarPorCodigo(int Codigo)
        {

            try
            {
                AbrirConexao();

                Cmd = new SqlCommand("SELECT * FROM PESSOA WHERE CODIGO = @V1", Con);

                Cmd.Parameters.AddWithValue("@V1", Codigo);
                Dr = Cmd.ExecuteReader();

                Pessoa p = null;

                if (Dr.Read())
                {
                    p = new Pessoa();

                    p.Codigo = Convert.ToInt32(Dr["Codigo"]);
                    p.Nome = Convert.ToString(Dr["Nome"]);
                    p.Endereco = Convert.ToString(Dr["Endereco"]);
                    p.Email = Convert.ToString(Dr["Email"]);
                }

                return p;
                
            }
            catch (Exception ex)
            {                
                throw new Exception("Erro ao buscar Pessoa: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        // Método para listar todos os clientes cadastrados
        public List<Pessoa> Listar()
        {
            try
            {
                AbrirConexao();

                Cmd = new SqlCommand("SELECT * FROM PESSOA", Con);
                Dr = Cmd.ExecuteReader();

                List<Pessoa> lista = new List<Pessoa>();

                while (Dr.Read())
                {
                    Pessoa p = new Pessoa();

                    p.Codigo = Convert.ToInt32(Dr["Codigo"]);
                    p.Nome = Convert.ToString(Dr["Nome"]);
                    p.Endereco = Convert.ToString(Dr["Endereco"]);
                    p.Email = Convert.ToString(Dr["Email"]);

                    lista.Add(p);
                }         

                return lista;
            }
            catch (Exception ex)
            {                
                throw new Exception("Erro ao listar as Pessoas: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

    }
}
