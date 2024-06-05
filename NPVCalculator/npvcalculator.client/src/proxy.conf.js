const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/api/calculator/npv",
      "/api/calculator/npv-range",
    ],
    target: "https://localhost:7086",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
