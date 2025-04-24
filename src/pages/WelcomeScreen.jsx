import React, { useState } from "react";
import { motion } from "framer-motion";
import { useNavigate } from "react-router-dom"; // useNavigate'ı import ediyoruz

const WelcomeScreen = () => {
  const navigate = useNavigate(); // Yönlendirme için useNavigate hook'u
  const [showWelcome, setShowWelcome] = useState(true); // Karşılama ekranını gösterip gizlemek için state

  const handleClick = () => {
    setShowWelcome(false);  // Ekranı gizle
    navigate("/home"); // "/home" sayfasına yönlendir
  };

  return (
    showWelcome && (  // Karşılama ekranını sadece state true olduğunda göster
      <div className="relative flex items-center justify-center h-screen bg-[#4D4C7D] text-white px-6">
        <motion.div
          initial={{ opacity: 0, y: -30 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.8, ease: "easeOut" }}
          className="max-w-5xl flex flex-col md:flex-row items-center justify-center gap-8 p-6 md:p-12"
        >
          {/* Sol Kısım */}
          <div className="w-full md:w-3/5 flex flex-col items-center text-center justify-center space-y-6">
            <h1 className="text-6xl font-light font-montserrat">CareerLink</h1>
            <p className="text-4xl font-montserrat leading-relaxed font-light tracking-tight max-w-2xl">
              Sana en uygun iş ilanlarını bulmaya hazırız. Hadi, kariyer basamaklarını bizimle tırmanmaya başla!
            </p>
            <motion.button
              whileHover={{ scale: 1.1 }}
              whileTap={{ scale: 0.9 }}
              onClick={handleClick} // Butona tıklandığında yönlendirme yapılacak
              className="px-8 py-4 bg-[#E9D5DA] text-[#363062] rounded-full text-xl font-bold shadow-md hover:bg-[#D8A7B1] transition font-montserrat"
            >
              İlk adımı at
            </motion.button>
          </div>

          {/* Sağ Kısım - GIF Animasyonu */}
          <motion.img
            src="img/animation.gif"
            alt="Career Growth Animation"
            className="w-[400px] h-[400px] md:w-[350px] md:h-[350px]"
          />
        </motion.div>
      </div>
    )
  );
};

export default WelcomeScreen;
