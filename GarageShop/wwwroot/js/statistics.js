
$(window).on('load', function () {
    getStatistics();
});

const getStatistics = () => {
    // Step 3
    var svg = d3.select("#pie-svg"),
        width = svg.attr("width"),
        height = svg.attr("height"),
        radius = 200;

    // Step 1        
    var data = [{ name: "Alex", share: 20.70 },
    { name: "Shelly", share: 30.92 },
    { name: "Clark", share: 15.42 },
    { name: "Matt", share: 13.65 },
    { name: "Jolene", share: 19.31 }];

    var g = svg.append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    // Step 4
    var ordScale = d3.scaleOrdinal()
        .domain(data)
        .range(['#ffd384', '#94ebcd', '#fbaccc', '#d3e0ea', '#fa7f72']);

    // Step 5
    var pie = d3.pie().value(function (d) {
        return d.share;
    });

    var arc = g.selectAll("arc")
        .data(pie(data))
        .enter();

    // Step 6
    var path = d3.arc()
        .outerRadius(radius)
        .innerRadius(0);

    arc.append("path")
        .attr("d", path)
        .attr("fill", function (d) { return ordScale(d.data.name); });

    // Step 7
    var label = d3.arc()
        .outerRadius(radius)
        .innerRadius(0);

    arc.append("text")
        .attr("transform", function (d) {
            return "translate(" + label.centroid(d) + ")";
        })
        .text(function (d) { return d.data.name; })
        .style("font-family", "arial")
        .style("font-size", 15);

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Bar Chart
    const sample = [
        {
            country: 'Rust',
            value: 78.9,
            color: 'red'
        },
        {
            country: 'Kotlin',
            value: 100,
            color: '#00a2ee'
        }
    ];

    // set the dimensions and margins of the graph
    var margin = { top: 30, right: 30, bottom: 70, left: 60 },
        width = 460 - margin.left - margin.right,
        height = 400 - margin.top - margin.bottom;

    // append the svg object to the body of the page
    const chartSvg = d3.select('#chart-svg')
        .append("svg")
        .attr("width", width + margin.left + margin.right)
        .attr("height", height + margin.top + margin.bottom)
        .append("g")
        .attr("transform",
            "translate(" + margin.left + "," + margin.top + ")");

  

        // X axis
        var x = d3.scaleBand()
            .range([0, width])
            .domain(sample.map(function (d) { return d.country; }))
            .padding(0.2);
        chartSvg.append("g")
            .attr("transform", "translate(0," + height + ")")
            .call(d3.axisBottom(x))
            .selectAll("text")
            .attr("transform", "translate(-10,0)rotate(-45)")
            .style("text-anchor", "end");

        // Add Y axis
        var y = d3.scaleLinear()
            .domain([0, 300])
            .range([height, 0]);
        chartSvg.append("g")
            .call(d3.axisLeft(y));

        // Bars
    chartSvg.selectAll("mybar")
        .data(sample)
            .enter()
            .append("rect")
            .attr("x", function (d) { return x(d.country); })
            .attr("y", function (d) { return y(d.value); })
            .attr("width", x.bandwidth())
        .attr("height", function (d) { return height - y(d.value); })
        .attr("fill", function (d) {  return d.color;})

}