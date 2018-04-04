namespace cyrka.api.domain.projects.commands.setPayments
{
	public class PaymentsSet : ProjectEventData
	{
		public readonly decimal TranslatorPayment;
		public readonly decimal EditorPayment;

		public PaymentsSet(string projectId, decimal translatorPayment, decimal editorPayment)
			: base(projectId)
		{
			TranslatorPayment = translatorPayment;
			EditorPayment = editorPayment;
		}
	}
}
