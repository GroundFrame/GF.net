using FluentValidation;
using GF.Common.SimSig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    /// <summary>
    /// Validator class to validate a <see cref="GFSimSigSimulationDTO"/> object
    /// </summary>
    internal class GFSimSigSimulationDTOValidator : AbstractValidator<GFSimSigSimulationDTO>
    {
        public GFSimSigSimulationDTOValidator()
        {
            RuleFor(sim => sim.Key).NotNull().NotEmpty();
            RuleFor(sim => sim.Name).NotNull().NotEmpty();
            RuleFor(sim => sim.SimSigCode).NotNull().NotEmpty();
        }
    }
}
