import {
  Avatar,
  Button,
  Flex,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalHeader,
  ModalOverlay,
} from "@chakra-ui/react";
import { useCallback, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { LoginForm } from "../LoginForm";
import { RegisterForm } from "../RegisterForm";

const NavBar = () => {
  const [openLogin, setOpenLogin] = useState(false);
  const [openRegister, setOpenRegister] = useState(false);
  const navigate = useNavigate();
  const [isAdmin, setIsAdmin] = useState(false);
  const token = localStorage.getItem("accessToken");

  const getRoles = useCallback(async () => {
    const data = await fetch("https://dentaclinic20221015140303.azurewebsites.net/api/me", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      method: "GET",
    });
    const roles = await data.json();
    if (roles.includes("Odontologist")) {
      setIsAdmin(true);
    } else {
      setIsAdmin(false);
    }
  }, []);

  useEffect(() => {
    getRoles();
  }, []);

  const Logout = (e: any): void => {
    e.preventDefault();
    localStorage.removeItem("accessToken");
    window.location.reload();
    navigate("/");
  };

  return (
    <>
      <Flex
        alignItems="center"
        bg="#232B3B"
        px={{ base: 4, md: 12 }}
        height={{ base: "150px", md: 20 }}
        shadow={{ base: "none", md: "lg" }}
        justifyContent={{ base: "center", md: "space-between" }}
        direction={{ base: "column", md: "row" }}
      >
        <Flex gap={10} direction={{ base: "column", md: "row" }} m={5}>
          {token && !isAdmin && (
            <Button onClick={() => navigate("/visitsHistory")}>
              Vizitų istorija
            </Button>
          )}
          {token && isAdmin && (
            <Button onClick={() => navigate("/visitsAdminPanel")}>
              Vizitų administravimas
            </Button>
          )}
          {token && !isAdmin && (
            <Button onClick={() => navigate("/freeVisits")}>
              Laisvi vizitai
            </Button>
          )}
        </Flex>
        <Flex
          gap={8}
          alignItems="center"
          direction={{ base: "column", md: "row" }}
        >
          {!token ? (
            <>
              <Button onClick={() => setOpenLogin(true)}>Prisijungti</Button>
              <Button onClick={() => setOpenRegister(true)}>
                Registruotis
              </Button>
            </>
          ) : (
            <>
              {!isAdmin && (
                <Avatar
                  style={{ cursor: "pointer" }}
                  onClick={() => navigate("/patientCard")}
                />
              )}
              <Button onClick={(e) => Logout(e)}>Atsijungti</Button>
            </>
          )}
        </Flex>
      </Flex>
      <Modal isOpen={openLogin} onClose={() => setOpenLogin(false)}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Prisijungimo forma</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <LoginForm closeModal={() => setOpenLogin(false)} />
          </ModalBody>
        </ModalContent>
      </Modal>
      <Modal isOpen={openRegister} onClose={() => setOpenRegister(false)}>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>Registracijos forma</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <RegisterForm closeModal={() => setOpenRegister(false)} />
          </ModalBody>
        </ModalContent>
      </Modal>
    </>
  );
};

export { NavBar };
