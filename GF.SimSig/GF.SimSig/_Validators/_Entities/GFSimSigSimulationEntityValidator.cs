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
    /// Validator class to validate a <see cref="GFSimSigSimulationEntity"/> object
    /// </summary>
    internal class GFSimSigSimulationEntityValidator : AbstractValidator<GFSimSigSimulationEntity>
    {
        public GFSimSigSimulationEntityValidator()
        {
            RuleFor(sim => sim!.Name).NotNull().NotEmpty();
            RuleFor(sim => sim!.SimSigCode).NotNull().NotEmpty();
        }
    }
}
