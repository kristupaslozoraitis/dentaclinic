import { Box, Button, Flex, Modal, ModalBody, ModalCloseButton, ModalContent, ModalHeader, ModalOverlay, Text, useDisclosure } from "@chakra-ui/react"
import React, { useState } from "react"
import { useNavigate } from "react-router-dom";

const NavBar = () => {
    const [openLogin, setOpenLogin] = useState(false);
    const [openRegister, setOpenRegister] = useState(false);
    const navigate = useNavigate();
    return (
        <>
            <Flex alignItems="center"
                bg="#232B3B" px={{ base: 4, md: 12 }}
                height={20}
                shadow="lg"
                justifyContent="space-between">
                <Flex>
                    <Button onClick={() => navigate("/visitsHistory")}>Vizitų istorija</Button>
                </Flex>
                <Flex gap={8}>
                    <Button onClick={() => setOpenLogin(true)}>Prisijungti</Button>
                    <Button>Atsijungti</Button>
                </Flex>
            </Flex>
            <Modal isOpen={openLogin} onClose={() => setOpenLogin(false)}>
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Login form</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody>
                        Test
                    </ModalBody>
                </ModalContent>
            </Modal>
        </>
    )
}

export { NavBar }