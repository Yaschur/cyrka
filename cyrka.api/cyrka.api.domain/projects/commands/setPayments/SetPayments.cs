namespace cyrka.api.domain.projects.commands.setPayments
{
	public class SetPayments
	{
		public readonly decimal TranslatorPayment;
		public readonly decimal EditorPayment;

		public SetPayments(decimal translatorPayment, decimal editorPayment)
		{
			TranslatorPayment = translatorPayment;
			EditorPayment = editorPayment;
		}
	}
}
