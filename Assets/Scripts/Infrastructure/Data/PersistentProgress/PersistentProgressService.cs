﻿using Infrastructure.Data.Serializable;

namespace Infrastructure.Data.PersistentProgress
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public GameProgress GameProgress { get; set; }
	}
}