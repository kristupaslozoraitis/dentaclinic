import { Avatar, Box, Button, Flex, Modal, ModalBody, ModalCloseButton, ModalContent, ModalHeader, ModalOverlay, Text, useDisclosure } from "@chakra-ui/react"
import React, { FormEvent, useState } from "react"
import { useNavigate } from "react-router-dom";
import { LoginForm } from "../LoginForm";
import { RegisterForm } from "../RegisterForm";

const NavBar = () => {
    const [openLogin, setOpenLogin] = useState(false);
    const [openRegister, setOpenRegister] = useState(false);
    const navigate = useNavigate();

    const Login = async (e: any): Promise<void> => {
        e.preventDefault();
        var token = localStorage.getItem("accessToken");
        const data = await fetch("https://localhost:7257/api/me", {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                method: 'GET',
        })
    }

    const goToPage = async (page:string) => {
        var token = localStorage.getItem("accessToken");
        const data = await fetch("https://localhost:7257/api/me", {
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                method: 'GET',
        })
        const roles = await data.json();
        if(!roles.includes("Odontologist")){

        } else{
            navigate(page)
        }
    }

    return (
        <>
            <Flex alignItems="center"
                bg="#232B3B" px={{ base: 4, md: 12 }}
                height={20}
                shadow="lg"
                justifyContent="space-between">
                <Flex gap={10}>
                    <Button onClick={() => navigate("/visitsHistory")}>Vizitų istorija</Button>
                    <Button onClick={() => goToPage("/visitsAdminPanel")}>Vizitų administravimas</Button>
                </Flex>
                <Flex gap={8}>
                    <Button onClick={() => setOpenLogin(true)}>Prisijungti</Button>
                    <Avatar style={{ cursor: 'pointer' }} onClick={() => navigate("/patientCard")} />
                    <Button onClick={(e) => Login(e)}>Atsijungti</Button>
                </Flex>
            </Flex>
            <Modal isOpen={openLogin} onClose={() => setOpenLogin(false)}>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Login form</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody>
                        <LoginForm />
                    </ModalBody>
                </ModalContent>
            </Modal>
        </>
    )
}

export { NavBar }