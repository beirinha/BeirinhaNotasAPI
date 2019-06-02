
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BeirinhaNotasAPI.Servicos
{
    public class InicializadorAPI : BackgroundService
    {
        int numeroVerificacoes = 0;
        
        public InicializadorAPI()
        {
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Debug.Write($"InicializadorAPI Iniciando...");

            stoppingToken.Register(() =>
                Debug.Write($" InicializadorAPI Parando"));

            while (!stoppingToken.IsCancellationRequested)
            {
                numeroVerificacoes++;
                Debug.Write($"Rodando Verificação: {numeroVerificacoes}");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            Debug.Write($"InicializadorAPI Parando");
        }
    }
}
