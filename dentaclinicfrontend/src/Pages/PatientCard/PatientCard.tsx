import { Box, Flex, FormControl, FormLabel, Heading, Input } from "@chakra-ui/react"
import React from "react"

const PatientCard = () => {
    return (
        <Box>
            <Box>
                <Heading textAlign="center">
                    Paciento kortelė
                </Heading>
            </Box>
            <FormControl>
                <Flex m={10} direction="row" justifyContent="center">
                    <Flex direction="column" mr={10}>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Vardas</FormLabel>
                            <Input type="text" value="Kristupas" />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Pavardė</FormLabel>
                            <Input type="text" value="Lozoraitis" />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Gimimo data</FormLabel>
                            <Input type="date" value="2000-11-25" />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Asmens kodas</FormLabel>
                            <Input type="text" value="123456789" />
                        </Flex>
                    </Flex>
                    <Flex direction="column" ml={10}>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Gyvenamasis adresas</FormLabel>
                            <Input type="text" value="Adresas" />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Telefonas</FormLabel>
                            <Input type="text" value="123456789" />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Ūgis</FormLabel>
                            <Input type="number" value="175" />
                        </Flex>
                        <Flex direction="column" w={200}>
                            <FormLabel textAlign="center">Svoris</FormLabel>
                            <Input type="number" value="60" />
                        </Flex>
                    </Flex>
                </Flex>
            </FormControl>
        </Box>
    )
}

export { PatientCard }