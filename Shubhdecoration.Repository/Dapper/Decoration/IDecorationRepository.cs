using Shubhdecoration.Data.Decoration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shubhdecoration.Repository.Dapper.Decoration
{
    public interface IDecorationRepository
    {
        Task<bool> CreateDecoCard(CreateCardModel model);
        Task<List<DecorationModel>> GetAllDecoration(int catId, int decoId);
    }
}
