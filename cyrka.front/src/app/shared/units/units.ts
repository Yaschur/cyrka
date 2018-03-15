import { TitleAbbr } from './title-abbr';

export enum Units {
	Undefined = 'Undefined',
	Piece = 'Piece',
	Minute = 'Minute',
	Character = 'Character',
	Symbol = 'Symbol',
	Gigabyte = 'Gigabyte',
}

export namespace Units {
	export const getTitle = (unit: Units): TitleAbbr => {
		switch (unit) {
			case Units.Piece:
				return { title: 'Штука/ единица', abbrevation: 'ед.' };
			case Units.Minute:
				return { title: 'Минута', abbrevation: 'мин.' };
			case Units.Character:
				return { title: 'Персонаж', abbrevation: 'перс.' };
			case Units.Symbol:
				return { title: 'Символ (текста)', abbrevation: 'симв.' };
			case Units.Gigabyte:
				return { title: 'Гигабайт', abbrevation: 'Гб.' };
			default:
				return { title: 'Не определено', abbrevation: '?' };
		}
	};
	export const all = [
		Units.Undefined,
		Units.Minute,
		Units.Piece,
		Units.Character,
		Units.Symbol,
		Units.Gigabyte,
	];
}
