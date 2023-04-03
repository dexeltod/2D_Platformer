﻿using System;

namespace Infrastructure.Services.Interfaces
{
    public interface IInputService : IService
    {
        event Action<float> VerticalButtonUsed;
        event Action VerticalButtonCanceled;
        event Action AttackButtonUsed;
        event Action InteractButtonUsed;
        event Action JumpButtonUsed;
        event Action JumpButtonCanceled;
    }
}