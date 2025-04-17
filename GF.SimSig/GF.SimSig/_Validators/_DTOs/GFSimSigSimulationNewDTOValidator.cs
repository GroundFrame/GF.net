using FluentValidation;
using GF.Common.SimSig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    internal class GFSimSigSimulationNewDTOValidator : AbstractValidator<GFSimSigSimulationNewDTO>
    {
        public GFSimSigSimulationNewDTOValidator()
        {
            RuleFor(sim => sim.Name).NotNull().NotEmpty();
            RuleFor(sim => sim.SimSigCode).NotNull().NotEmpty();
        }
    }
}
