type FormatMoneyType = { amount: number };
export const Money = ({ amount }: FormatMoneyType) => <>{amount.toFixed(2)}</>;
