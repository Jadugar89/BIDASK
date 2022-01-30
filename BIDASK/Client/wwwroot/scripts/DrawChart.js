
export function DrawChart(Object)
{

    let args = Array.from(Object);
    var c = document.getElementById("myCanvas");

    var ctx = c.getContext("2d");
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
            datasets: [{
                label: '# of Votes',
                data: [12, 19, 3, 5, 2, 3],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function DrawCandle(ctx, Open,Close )
{
    var distans = Math.abs(Close - Open);
    var IsPositive = Close > Open;

    const width = distans,
          height = 50;

    // starting point
    const x = 0,
        y = 0;
    debugger;
    ctx.scale(0.5, 0.5);
    ctx.strokeStyle = 'red';
    ctx.strokeRect(x, y, width, height);
    


    

}
