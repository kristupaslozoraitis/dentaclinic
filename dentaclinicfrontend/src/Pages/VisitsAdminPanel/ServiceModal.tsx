import { CheckIcon, CloseIcon, EditIcon } from '@chakra-ui/icons'
import { Modal, ModalOverlay, ModalContent, ModalHeader, Divider, ModalCloseButton, ModalBody, List, ListItem, Flex, Editable, EditablePreview, EditableInput, ButtonGroup, IconButton, useEditableControls, Button } from '@chakra-ui/react'
import React, { FunctionComponent, ReactElement } from 'react'
import { Service } from './types'

interface ServiceModalProps {
    isOpen: boolean,
    closeModal: () => void,
    services: Service[],
    save: () => Promise<void>,
    handleServiceChange: (value:string, index:number) => void,
}

const ServiceModal = ({ isOpen, closeModal, services, save, handleServiceChange }: ServiceModalProps): ReactElement<ServiceModalProps> => {
    const EditableControls = () => {
        const {
            isEditing,
            getSubmitButtonProps,
            getCancelButtonProps,
            getEditButtonProps,
        } = useEditableControls()
        return isEditing ? (
            <ButtonGroup size='sm' mt={3}>
                <IconButton aria-label='Save' icon={<CheckIcon />} {...getSubmitButtonProps()}>Save</IconButton>
                <IconButton aria-label='Close' icon={<CloseIcon />} {...getCancelButtonProps()}>Atsaukti</IconButton>
            </ButtonGroup>
        ) : (
            <IconButton ml={5} size='sm' aria-label='Edit' icon={<EditIcon />} {...getEditButtonProps()}>Edit</IconButton>
        )
    }
    return (
        <Modal isOpen={isOpen} onClose={closeModal}>
            <ModalOverlay />
            <ModalContent>
                <ModalHeader>Paslaugos</ModalHeader>
                <Divider />
                <ModalCloseButton />
                <ModalBody mb={5}>
                    <List spacing={3}>
                        {services.map((service, index) => {
                            return (
                                <ListItem key={index}>
                                    <Flex gap={5}>
                                        <Editable
                                            textAlign='left'
                                            defaultValue={service.name}
                                            fontSize='lg'
                                            isPreviewFocusable={false}
                                            onChange={(value) => handleServiceChange(value, index)}
                                        >
                                            <EditablePreview />
                                            <EditableInput />
                                            <EditableControls />
                                        </Editable>
                                    </Flex>
                                </ListItem>
                            )
                        })}
                    </List>
                    <Button onClick={() => save()}>Saugoti</Button>
                </ModalBody>
            </ModalContent>
        </Modal>
    )
}

export { ServiceModal }