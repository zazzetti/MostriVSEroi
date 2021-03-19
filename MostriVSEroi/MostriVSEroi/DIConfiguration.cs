using Microsoft.Extensions.DependencyInjection;
using MostriVSEroi.ADORepository;
using MostriVSEroi.Core.Interfaces;
using MostriVSEroi.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVSEroi
{

    public class DIConfiguration
    {

        public static ServiceProvider Configurazione()
        {

            return new ServiceCollection()

                .AddScoped<GiocatoreService>()
                .AddScoped<EroeService>()
                .AddScoped<MostroService>()
                .AddScoped<LivelloService>()

                .AddScoped<IMostroRepository, ADOMostroRepository>()
                .AddScoped<IEroeRepository, ADOEroeRepository>()
                .AddScoped<IClasseRepository, ADOClasseRepository>()
                .AddScoped<IArmaRepository, ADOArmaRepository>()
                .AddScoped<ILivelloRepository, ADOLivelloRepository>()
                .AddScoped<IGiocatoreRepository, ADOGiocatoreRepository>()

                .BuildServiceProvider();

        }
    }
}
