using System;
using Core;
namespace ServerApp.Repositories
{
	public interface IBikeRepository
	{
		Bike[] GetAll();
		void Add(Bike bike);
		void Delete(int id);
	}
}

