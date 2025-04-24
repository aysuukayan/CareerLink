/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        primary: "#E9D5DA",
        secondary: "#827397",
        accent: "#4D4C7D",
        dark: "#363062",
      },
      fontFamily: {
        montserrat: ["Montserrat", "sans-serif"],
    },
  },
  plugins: [],
}
}
