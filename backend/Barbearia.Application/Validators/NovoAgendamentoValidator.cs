using Barbearia.Application.DTOs.Agendamento;
using FluentValidation;

namespace Barbearia.Application.Validators
{
    public class NovoAgendamentoValidator : AbstractValidator<NovoAgendamentoDTO>
    {
        public NovoAgendamentoValidator()
        {
            RuleFor(x => x.DataHorario)
                .NotEmpty().WithMessage("A data e hora são obrigatórias")
                .Must(BeAValidDateTime).WithMessage("A data deve ser futura")
                .Must(BeInBusinessHours).WithMessage("Horário fora do expediente");

            RuleFor(x => x.ServicosIds)
                .NotEmpty().WithMessage("Selecione pelo menos um serviço")
                .Must(list => list != null && list.Any()).WithMessage("Selecione pelo menos um serviço");

            RuleFor(x => x.ObservacaoCliente)
                .MaximumLength(500).WithMessage("A observação não pode ter mais que 500 caracteres");
        }

        private bool BeAValidDateTime(DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }

        private bool BeInBusinessHours(DateTime dateTime)
        {
            return dateTime.Hour >= 8 && dateTime.Hour < 23 &&
                   dateTime.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
