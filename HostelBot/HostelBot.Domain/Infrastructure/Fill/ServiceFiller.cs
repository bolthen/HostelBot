﻿using System.Text.Json;

namespace HostelBot.Domain.Infrastructure;

public class ServiceFiller : IFiller
{
    public ICanFill GetFillClass()
    {
        return new Service();
    }

    public void HandleFilledClass(string data)
    {
        JsonSerializer.Deserialize<Service>(data);
    }
}