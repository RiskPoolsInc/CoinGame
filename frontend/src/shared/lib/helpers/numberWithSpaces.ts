export default (x: string | number) =>  {
    return parseFloat(x.toString().replace(/[^0-9.]/g, '')).toLocaleString('cs-CZ');
}
