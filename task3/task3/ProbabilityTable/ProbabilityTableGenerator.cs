using Spectre.Console;

namespace task3.ProbabilityTable
{
    internal class ProbabilityTableGenerator
    {
        public Table GenerateTable(List<Dice> dice)
        {
            var table = new Table();
            Title(table);
            Column(table,dice);
            Row(table,dice);
            return table;
        }

        private void Title(Table table) {
            table.Title = new TableTitle("[red][bold]Probability of the win for the user[/][/]");
        }

        private void Column(Table table, List<Dice> dice){
            table.Border = TableBorder.Rounded;
            table.AddColumn(new TableColumn("[bold]User dice v[/]").Centered());
            foreach (var d in dice)
                table.AddColumn(new TableColumn($"[blue][bold]{Markup.Escape(d.ToString())}[/][/]").Centered());
        }
        private void Row(Table table,List<Dice> dice) {
            for (int i = 0; i < dice.Count; i++)
            {
                var cells = new List<string> { $"[blue]{Markup.Escape(dice[i].ToString())}[/]" };
                for (int j = 0; j < dice.Count; j++)
                {

                    double prob = ProbabilityCalculator.CalculateWinProbability(dice[i], dice[j]);
                    cells.Add(prob.ToString("F2"));
                }
                table.AddRow(cells.ToArray());
            }
        }
    }

}